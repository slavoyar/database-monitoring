using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Data;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Controllers
{
    /// <summary>
    /// Controller for Authenticate
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Constructor and Common vars

        private const string appFriendlyName = "AuthWebApp";
        private const string jwtAccesstokenName = "JwtAccessToken";
        private const string jwtRefreshtokenName = "JwtRefreshToken";
        private readonly AuthDbContext _authWebApplicationDbContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AuthUser> _userManager;

        public AuthController(
            UserManager<AuthUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            AuthDbContext authWebApplicationDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authWebApplicationDbContext = authWebApplicationDbContext;
        }

        #endregion Constructor and Common vars

        #region Login

        /// <summary>
        /// Get JWT Access token + RefreshToken for further login
        /// </summary>
        /// <param name="model">Model of data for Login</param>
        /// <response code="200">JWT Access token + expiration Date </response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponse), 200)]
        [ProducesResponseType(typeof(WebResponse), 400)]
        [ProducesResponseType(typeof(WebResponse), 401)]
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginModel model)
        {
            var modelEmail = model.Email;
            var modelPass = model.Password;

            //--- Check Input Data
            if (modelEmail == null)
                return BadRequest(WebResponsesAuth.authResponseErrorEmail);

            var loggingUser = await _userManager.FindByEmailAsync(modelEmail);

            if (loggingUser == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            if (modelPass == null)
                return BadRequest(WebResponsesAuth.authResponseErrorPassword);

            var checkPass = await _userManager.CheckPasswordAsync(loggingUser, modelPass);

            if (!checkPass)
                return BadRequest(WebResponsesAuth.authResponseErrorPassword);

            //--- Delete old Tokens from Database
            await _userManager.RemoveAuthenticationTokenAsync(loggingUser, appFriendlyName, jwtAccesstokenName);

            //--- Get all user Claims
            var userRoles = await _userManager.GetRolesAsync(loggingUser);

            var authClaims = new List<Claim>();

            if (loggingUser.FullUserName != null)
            {
                var claimFullUserName = new Claim(ClaimTypes.Name, loggingUser.FullUserName);
                authClaims.Add(claimFullUserName);
            }

            if (loggingUser.Email != null)
            {
                var claimEmail = new Claim(ClaimTypes.Email, loggingUser.Email);
                authClaims.Add(claimEmail);
            }

            foreach (var userRole in userRoles)
            {
                var claimRole = new Claim(ClaimTypes.Role, userRole);
                authClaims.Add(claimRole);
            }

            var currentRefreshToken = await _userManager.GetAuthenticationTokenAsync(loggingUser, appFriendlyName, jwtRefreshtokenName);

            //--- Create JWT Access Token
            var JwtAccessToken = GenerateJwtAccessToken(authClaims);

            if (JwtAccessToken != null)
            {
                var JwtAccessTokenHashed = new JwtSecurityTokenHandler().WriteToken(JwtAccessToken);
                await _userManager.SetAuthenticationTokenAsync(loggingUser, appFriendlyName, jwtAccesstokenName, JwtAccessTokenHashed);

                var timeNow = DateTime.UtcNow;
                var restTime = loggingUser.RefreshTokenExpiryTime - timeNow;
                _ = int.TryParse(_configuration["JWT:RefreshTokenLastValidityInHours"], out int refreshTokenLastValidityInHours);
                if (restTime.TotalHours <= refreshTokenLastValidityInHours)
                {
                    await _userManager.RemoveAuthenticationTokenAsync(loggingUser, appFriendlyName, jwtRefreshtokenName);

                    var newRefreshToken = await _userManager.GenerateUserTokenAsync(loggingUser, appFriendlyName, jwtRefreshtokenName);
                    await _userManager.SetAuthenticationTokenAsync(loggingUser, appFriendlyName, jwtRefreshtokenName, newRefreshToken);

                    currentRefreshToken = newRefreshToken;

                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                    loggingUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                    await _userManager.UpdateAsync(loggingUser);
                }

                var user = new AuthUpdateModel
                {
                    Id = loggingUser.Id,
                    FullUserName = loggingUser.FullUserName,
                    Email = loggingUser.Email,
                    PhoneNumber = loggingUser.PhoneNumber
                };

                return Ok
                (new
                {
                    JwtAccessToken = JwtAccessTokenHashed,
                    JwtRefreshToken = currentRefreshToken,
                    user = user,
                });
            }

            return Unauthorized(WebResponsesAuth.authResponseErrorUnauthorized);
        }

        #endregion Login

        #region UpdateAccessToken

        /// <summary>
        /// Get JWT refresh token for further login
        /// </summary>
        /// <param name="tokenModel">Model of token data</param>
        /// <response code="200">JWT Access token + JWT Access expiration Date + JWT Refresh token + JWT Refresh expiration Date </response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponse), 200)]
        [ProducesResponseType(typeof(WebResponse), 400)]
        [ProducesResponseType(typeof(WebResponse), 401)]
        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> UpdateAccessToken(TokenModel tokenModel)
        {
            //--- Check Input Data
            if (tokenModel is null)
                return BadRequest(WebResponsesAuth.authResponseErrorToken);

            string? accessToken = tokenModel.AccessToken;
            if (accessToken == null)
                return BadRequest(WebResponsesAuth.authResponseErrorAccessToken);

            string? refreshToken = tokenModel.RefreshToken;
            if (refreshToken == null)
                return BadRequest(WebResponsesAuth.authResponseErrorRefreshToken);

            var principal = GetPrincipalFromToken(accessToken);
            var princimalClaims = principal?.Claims.ToList();
            if (principal == null || princimalClaims == null)
                return BadRequest(WebResponsesAuth.authResponseErrorAccessToken);

            var currentUserEmail = principal.FindFirstValue(ClaimTypes.Email);
            if (currentUserEmail == null)
                return BadRequest(WebResponsesAuth.authResponseErrorClaimsPrincipal);

            var refreshUser = await _userManager.FindByEmailAsync(currentUserEmail);
            if (refreshUser == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            var isValid = await _userManager.VerifyUserTokenAsync(refreshUser, appFriendlyName, jwtRefreshtokenName, refreshToken);

            if (!isValid || refreshUser.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest(WebResponsesAuth.authResponseErrorRefreshToken);

            //--- Delete old Tokens from Database
            await _userManager.RemoveAuthenticationTokenAsync(refreshUser, appFriendlyName, jwtAccesstokenName);
            await _userManager.RemoveAuthenticationTokenAsync(refreshUser, appFriendlyName, jwtRefreshtokenName);

            //--- Create JWT Access Token
            var newAccessToken = GenerateJwtAccessToken(princimalClaims);

            if (newAccessToken != null)
            {
                var JwtAccessTokenHashed = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
                await _userManager.SetAuthenticationTokenAsync(refreshUser, appFriendlyName, jwtAccesstokenName, JwtAccessTokenHashed);

                //--- Create new JWT Refresh Token
                var newRefreshToken = await _userManager.GenerateUserTokenAsync(refreshUser, appFriendlyName, jwtRefreshtokenName);
                await _userManager.SetAuthenticationTokenAsync(refreshUser, appFriendlyName, jwtRefreshtokenName, newRefreshToken);

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                refreshUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                await _userManager.UpdateAsync(refreshUser);

                return new ObjectResult(new
                {
                    JwtAccessToken = JwtAccessTokenHashed,
                    JwtRefreshToken = newRefreshToken,
                });
            }
            return Unauthorized(WebResponsesAuth.authResponseErrorUnauthorized);
        }

        #endregion UpdateAccessToken

        #region Usefull Methods

        /// <summary>
        /// Create JWT Access token with input claims
        /// </summary>
        /// <param name="authClaims">All Claims for Auth</param>
        /// <returns></returns>
        private JwtSecurityToken? GenerateJwtAccessToken(List<Claim> authClaims)
        {
            //--- Get JWT Secret Token
            var jwtSecret = _configuration["JWT:Secret"];

            if (jwtSecret == null)
                return null;

            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            //--- Create JWT Access Token
            var secretKey = Encoding.UTF8.GetBytes(jwtSecret);
            var authSigningKey = new SymmetricSecurityKey(secretKey);

            var tokenSigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: tokenSigningCredentials
                );

            return token;
        }

        /// <summary>
        /// Get Claims Data from Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        private ClaimsPrincipal? GetPrincipalFromToken(string? token)
        {
            //--- Get JWT Secret Token
            var jwtSecret = _configuration["JWT:Secret"];

            if (jwtSecret == null)
                return null;

            //--- Parse token
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
                )
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        #endregion Usefull Methods
    }
}