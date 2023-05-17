using Auth.Data;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        #region CheckUserExisting_Test

        /// <summary>
        /// Check if User exist if DB ( method for testing )
        /// </summary>
        /// <param name="model">Model of data for Register</param>
        /// <response code="200">Success check</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("CheckUserExisting_Test")]
        public async Task<IActionResult> CheckUserExisting_Test([FromBody] AuthLoginModel model)
        {
            // Some Procedure for testing

            //--- Check Input Data
            var modelEmail = model.Email;
            if ( modelEmail == null )
                return BadRequest(WebResponcesAuth.authResponceErrorEmail);

            //--- Check User Exists
            var userExists = await _userManager.FindByEmailAsync(modelEmail);
            if ( userExists != null )
            {
                // Eager Loading for loading Inner Classes from database (like Workspaces in Users)
                var zxczxc321 = _authWebApplicationDbContext.Users.Include(x => x.Workspaces).ToList();

                var wks = userExists.Workspaces;

                var wks2 = _authWebApplicationDbContext.Workspaces;

                var workspace = new Workspaces()
                {
                    WorkspacesId = Guid.NewGuid().ToString(),
                    Description = "AllInOne Admin Workspace",
                    Name = "Eye Of Sauron Workspace"
                };

                _authWebApplicationDbContext.Workspaces.Add(workspace);
                await _authWebApplicationDbContext.SaveChangesAsync();

                if ( workspace.Users is null )
                {
                    var userList = new List<AuthUser>() { userExists };
                    workspace.Users = userList;
                    userExists.Workspaces.Add(workspace);
                }
                else
                {
                    workspace.Users.Add(userExists);
                }

                await _userManager.UpdateAsync(userExists);

                return Ok(WebResponcesAuth.authResponceSuccessUserExist);
            }

            return BadRequest();
        }

        #endregion CheckUserExisting_Test

        #region GetAccessToken

        /// <summary>
        /// Get JWT Access token for further login
        /// </summary>
        /// <param name="model">Model of data for Login</param>
        /// <response code="200">JWT Access token + expiration Date </response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [AllowAnonymous]
        [HttpPost]
        [Route("GetAccessToken")]
        public async Task<IActionResult> GetAccessToken([FromBody] AuthLoginModel model)
        {
            var modelEmail = model.Email;
            var modelPass = model.Password;

            //--- Check Input Data
            if ( modelEmail == null )
                return BadRequest(WebResponcesAuth.authResponceErrorEmail);

            var loggingUser = await _userManager.FindByEmailAsync(modelEmail);

            if ( loggingUser == null )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            if ( modelPass == null )
                return BadRequest(WebResponcesAuth.authResponceErrorPassword);

            var checkPass = await _userManager.CheckPasswordAsync(loggingUser, modelPass);

            if ( !checkPass )
                return BadRequest(WebResponcesAuth.authResponceErrorPassword);

            //--- Delete old Tokens from Database
            await _userManager.RemoveAuthenticationTokenAsync(loggingUser, appFriendlyName, jwtAccesstokenName);
            await _userManager.RemoveAuthenticationTokenAsync(loggingUser, appFriendlyName, jwtRefreshtokenName);

            //--- Get all user Claims
            var userRoles = await _userManager.GetRolesAsync(loggingUser);

            var authClaims = new List<Claim>();

            var userWorkspaces = _authWebApplicationDbContext.Users
                .Where(user => user == loggingUser)
                .SelectMany(atr => atr.Workspaces)
                .ToList();

            foreach ( var workspace in userWorkspaces )
            {
                if ( workspace?.Name != null )
                {
                    var claimWorkSpace = new Claim(CustomClaims.WorkSpaces, workspace.Name.ToString());
                    authClaims.Add(claimWorkSpace);
                }
            }

            if ( loggingUser.FullUserName != null )
            {
                var claimFullUserName = new Claim(ClaimTypes.Name, loggingUser.FullUserName);
                authClaims.Add(claimFullUserName);
            }

            foreach ( var userRole in userRoles )
            {
                var claimRole = new Claim(ClaimTypes.Role, userRole);
                authClaims.Add(claimRole);
            }

            //--- Create JWT Access Token
            var JwtAccessToken = GenerateJwtAccessToken(authClaims);

            if ( JwtAccessToken != null )
            {
                var JwtAccessTokenHashed = new JwtSecurityTokenHandler().WriteToken(JwtAccessToken);
                await _userManager.SetAuthenticationTokenAsync(loggingUser, appFriendlyName, jwtAccesstokenName, JwtAccessTokenHashed);

                var newRefreshToken = await _userManager.GenerateUserTokenAsync(loggingUser, appFriendlyName, jwtRefreshtokenName);
                await _userManager.SetAuthenticationTokenAsync(loggingUser, appFriendlyName, jwtRefreshtokenName, newRefreshToken);

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                loggingUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                await _userManager.UpdateAsync(loggingUser);

                return Ok
                (new
                {
                    JwtAccessToken = JwtAccessTokenHashed,
                    JwtAccessTokenExpirationDate = JwtAccessToken.ValidTo,
                    JwtRefreshToken = newRefreshToken,
                    JwtRefreshTokenExpirationDate = loggingUser.RefreshTokenExpiryTime
                });
            }

            return Unauthorized(WebResponcesAuth.authResponceErrorUnauthorized);
        }

        #endregion GetAccessToken

        #region GetRefreshToken

        /// <summary>
        /// Get JWT refresh token for further login
        /// </summary>
        /// <param name="tokenModel">Model of token data</param>
        /// <response code="200">JWT Access token + JWT Access expiration Date + JWT Refresh token + JWT Refresh expiration Date </response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [Authorize]
        [HttpPost]
        [Route("GetRefreshToken")]
        public async Task<IActionResult> GetRefreshToken(TokenModel tokenModel)
        {
            //--- Check Input Data
            if ( tokenModel is null )
                return BadRequest(WebResponcesAuth.authResponceErrorToken);

            string? accessToken = tokenModel.AccessToken;
            if ( accessToken == null )
                return BadRequest(WebResponcesAuth.authResponceErrorAccessToken);

            string? refreshToken = tokenModel.RefreshToken;
            if ( refreshToken == null )
                return BadRequest(WebResponcesAuth.authResponceErrorRefreshToken);

            var principal = GetPrincipalFromExpiredToken(accessToken);
            var princimalClaims = principal?.Claims.ToList();
            if ( principal == null || princimalClaims == null )
                return BadRequest(WebResponcesAuth.authResponceErrorAccessToken);

            string? username = principal?.Identity?.Name;

            if ( username == null )
                return BadRequest(WebResponcesAuth.authResponceErrorClaimsPrincipal);

            var refreshUser = await _userManager.FindByNameAsync(username);
            if ( refreshUser == null )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            var isValid = await _userManager.VerifyUserTokenAsync(refreshUser, appFriendlyName, jwtRefreshtokenName, refreshToken);

            if ( !isValid || refreshUser.RefreshTokenExpiryTime <= DateTime.Now )
                return BadRequest(WebResponcesAuth.authResponceErrorRefreshToken);

            //--- Delete old Tokens from Database
            await _userManager.RemoveAuthenticationTokenAsync(refreshUser, appFriendlyName, jwtAccesstokenName);
            await _userManager.RemoveAuthenticationTokenAsync(refreshUser, appFriendlyName, jwtRefreshtokenName);

            //--- Create JWT Access Token
            var newAccessToken = GenerateJwtAccessToken(princimalClaims);

            if ( newAccessToken != null )
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
                    JwtAccessTokenExpirationDate = newAccessToken.ValidTo,
                    JwtRefreshToken = newRefreshToken,
                    JwtRefreshTokenExpirationDate = refreshUser.RefreshTokenExpiryTime
                });
            }
            return Unauthorized(WebResponcesAuth.authResponceErrorUnauthorized);
        }

        #endregion GetRefreshToken

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

            if ( jwtSecret == null )
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
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            //--- Get JWT Secret Token
            var jwtSecret = _configuration["JWT:Secret"];

            if ( jwtSecret == null )
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
            if ( securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
                )
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        #endregion Usefull Methods
    }
}