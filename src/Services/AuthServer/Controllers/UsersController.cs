using System.Data;
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

        #region Create

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
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] AuthRegisterModel model)
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

        #endregion Create

        #region Read

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
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Read()
        {
            //--- Check Input Data
            var foundedUsers = _userManager
                .Users
                .Where(user => user.Role != UserRoles.Admin)
                .ToList();

            if (foundedUsers == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            //--- Return found Users
            return Ok(foundedUsers);
        }

        /// <summary>
        /// Get user with input mail
        /// </summary>
        /// <param name="userMail"></param>
        /// <returns></returns>
        /// <response code="200">Success reading</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponse), 200)]
        [ProducesResponseType(typeof(WebResponse), 400)]
        [ProducesResponseType(typeof(WebResponse), 401)]
        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> Read([FromBody] string userMail)
        {
            //--- Check Input Data
            var foundedUser = await _userManager.FindByEmailAsync(userMail);

            // User cannot found Admin user
            if (foundedUser == null || foundedUser?.Role == UserRoles.Admin)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            //--- Return found user
            return Ok(foundedUser);
        }

        #endregion Read

        #region Update

        /// <summary>
        /// Update user with input mail
        /// </summary>
        /// <param name="userMail">Name of user to update</param>
        /// <param name="inputUser">New model data for updating user</param>
        /// <returns></returns>
        /// <response code="200">Success updating</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponse), 200)]
        [ProducesResponseType(typeof(WebResponse), 400)]
        [ProducesResponseType(typeof(WebResponse), 401)]
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(string userMail, [FromBody] AuthRegisterModel inputUser)
        {
            //--- Check Input Data
            var foundedUser = await _userManager.FindByEmailAsync(userMail);

            if (foundedUser == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            //--- Check Input Data
            var userRole = inputUser.Role;
            if (userRole == null)
                return BadRequest(WebResponsesAuth.authResponseErrorRole);

            if (!await _roleManager.RoleExistsAsync(userRole))
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponsesAuth.authResponseErrorNoRoleInDb);

            // User cannot found Admin mail
            if (userRole == UserRoles.Admin)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            //--- Updating data of user

            foundedUser.Email = inputUser.Email;
            foundedUser.FullUserName = inputUser.FullUserName;
            foundedUser.Role = userRole;
            foundedUser.PhoneNumber = inputUser.PhoneNumber;
            foundedUser.Password = inputUser.Password;
            foundedUser.UserName = inputUser.Email;

            var result = await _userManager.UpdateAsync(foundedUser);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponsesAuth.authResponseErrorUserUpdate);

            return Ok(WebResponsesAuth.authResponseSuccessUserUpdate);
        }

        #endregion Update

        #region Delete

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
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] string userMail)
        {
            //--- Check Input Data
            var foundedUser = await _userManager.FindByEmailAsync(userMail);

            // User cannot found Admin user
            if (foundedUser == null)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            if (foundedUser.Role == UserRoles.Admin)
                return BadRequest(WebResponsesAuth.authResponseErrorUser);

            var result = await _userManager.DeleteAsync(foundedUser);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponsesAuth.authResponseSuccessUserDelete);

            return Ok(WebResponsesAuth.authResponseSuccessUserDelete);
        }

        #endregion Delete
    }
}