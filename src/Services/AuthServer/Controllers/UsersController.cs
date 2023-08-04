using System.Data;
using System.Security.Claims;
using Auth.Data;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Constructor and Common vars

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AuthUser> _userManager;

        public UsersController(
            UserManager<AuthUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #endregion Constructor and Common vars

        #region CreateUser

        /// <summary>
        /// Create User with input data
        /// </summary>
        /// <param name="model">Model of data for Register</param>
        /// <response code="200">User created successfully</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="500">Database data error</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponse), 200)]
        [ProducesResponseType(typeof(WebResponse), 400)]
        [ProducesResponseType(typeof(WebResponse), 401)]
        [ProducesResponseType(typeof(WebResponse), 500)]
        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] AuthRegisterModel model)
        {
            //--- Check Input Data
            var userRole = model.Role;
            if (userRole == null)
                return BadRequest(WebResponsesAuth.authResponseErrorRole);

            var modelEmail = model.Email;
            if (modelEmail == null)
                return BadRequest(WebResponsesAuth.authResponseErrorEmail);

            var userExists = await _userManager.FindByEmailAsync(modelEmail);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponsesAuth.authResponseErrorUserExist);

            if (!await _roleManager.RoleExistsAsync(userRole))
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponsesAuth.authResponseErrorNoRoleInDb);

            // User cannot create Admin
            if (userRole == UserRoles.Admin)
                return BadRequest(WebResponsesAuth.authResponseErrorRole);

            //--- Create User + Add Role to User
            AuthUser user = new()
            {
                Email = model.Email,
                FullUserName = model.FullUserName,
                Role = userRole,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponsesAuth.authResponseErrorUserCreate);

            if (await _roleManager.RoleExistsAsync(userRole))
                await _userManager.AddToRoleAsync(user, userRole);

            return Ok(WebResponsesAuth.authResponseSuccessUserCreate);
        }

        #endregion CreateUser

        #region GetUsers

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success reading</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponse), 200)]
        [ProducesResponseType(typeof(WebResponse), 400)]
        [ProducesResponseType(typeof(WebResponse), 401)]
        [Authorize]
        [HttpGet]
        public ActionResult<List<AuthUpdateModel>> GetUsers()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var email = User.FindFirstValue(ClaimTypes.Email);

            var allUsers = _userManager.Users;
            if (allUsers == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            if (role != UserRoles.Admin)
                return Ok(allUsers.Where(user => user.Role != UserRoles.Admin).ToList());

            return Ok(allUsers.Select(user => new AuthUpdateModel
            {
                Id = user.Id,
                FullUserName = user.FullUserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = foundedUser.Role
            }));
        }

        /// <summary>
        /// Get user by email from the claims
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success reading</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponse), 200)]
        [ProducesResponseType(typeof(WebResponse), 400)]
        [ProducesResponseType(typeof(WebResponse), 401)]
        [Authorize]
        [HttpGet]
        [Route("info")]
        public async Task<ActionResult<AuthUpdateModel>> GetUserInfo()
        {
            var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);
            if (currentUserEmail == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            var foundedUser = await _userManager.FindByEmailAsync(currentUserEmail);
            if (foundedUser == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            return Ok(new AuthUpdateModel
            {
                Id = foundedUser.Id,
                FullUserName = foundedUser.FullUserName,
                Email = foundedUser.Email,
                PhoneNumber = foundedUser.PhoneNumber,
                Role = foundedUser.Role
            });
        }

        #endregion GetUsers

        #region UpdateUser

        /// <summary>
        /// Update user with input mail
        /// </summary>
        /// <param name="inputUser">New model data for updating user</param>
        /// <returns></returns>
        /// <response code="200">Success updating</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponse), 200)]
        [ProducesResponseType(typeof(WebResponse), 400)]
        [ProducesResponseType(typeof(WebResponse), 401)]
        [Authorize]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] AuthUpdateModel inputUser)
        {
            if (inputUser.Email == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            //--- Check if user exists
            var foundedUser = await _userManager.FindByEmailAsync(inputUser.Email);

            if (foundedUser == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            //--- User can not edit other users if its not admin
            var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);
            if (inputUser.Email != currentUserEmail)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            //--- Updating data of user
            foundedUser.Email = inputUser.Email;
            foundedUser.UserName = inputUser.Email;
            foundedUser.FullUserName = inputUser.FullUserName;
            foundedUser.PhoneNumber = inputUser.PhoneNumber;

            if (inputUser.Password != null)
                foundedUser.Password = inputUser.Password;

            var result = await _userManager.UpdateAsync(foundedUser);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponsesAuth.authResponseErrorUserUpdate);

            return Ok(WebResponsesAuth.authResponseSuccessUserUpdate);
        }

        #endregion UpdateUser

        #region DeleteUser

        /// <summary>
        /// Delete user with input mail
        /// </summary>
        /// <param name="userMail">Name of user to Delete</param>
        /// <returns></returns>
        /// <response code="200">Success deleting</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponse), 200)]
        [ProducesResponseType(typeof(WebResponse), 400)]
        [ProducesResponseType(typeof(WebResponse), 401)]
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] string userMail)
        {
            var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);
            if (currentUserEmail == userMail)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            var foundedUser = await _userManager.FindByEmailAsync(userMail);

            if (foundedUser == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            if (foundedUser.Role == UserRoles.Admin)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            var result = await _userManager.DeleteAsync(foundedUser);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponsesAuth.authResponseSuccessUserDelete);

            return Ok(WebResponsesAuth.authResponseSuccessUserDelete);
        }

        #endregion DeleteUser
    }
}