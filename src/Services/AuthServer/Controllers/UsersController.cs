using Auth.Data;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [ProducesResponseType(typeof(WebResponce), 500)]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AuthRegisterModel model)
        {
            //--- Check Input Data
            var userRole = model.Role;
            if ( userRole == null )
                return BadRequest(WebResponcesAuth.authResponceErrorRole);

            var modelEmail = model.Email;
            if ( modelEmail == null )
                return BadRequest(WebResponcesAuth.authResponceErrorEmail);

            var userExists = await _userManager.FindByEmailAsync(modelEmail);
            if ( userExists != null )
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponcesAuth.authResponceErrorUserExist);

            if ( !await _roleManager.RoleExistsAsync(userRole) )
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponcesAuth.authResponceErrorNoRoleInDb);

            // User cannot create Admin
            if ( userRole == UserRoles.Admin )
                return BadRequest(WebResponcesAuth.authResponceErrorRole);

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

            if ( !result.Succeeded )
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponcesAuth.authResponceErrorUserCreate);

            if ( await _roleManager.RoleExistsAsync(userRole) )
                await _userManager.AddToRoleAsync(user, userRole);

            return Ok(WebResponcesAuth.authResponceSuccessUserCreate);
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
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpGet]
        [Route("Read")]
        public async Task<IActionResult> Read()
        {
            //--- Check Input Data
            var foundedUsers = _userManager
                .Users
                .Where(user => user.Role != UserRoles.Admin)
                .ToList();

            if ( foundedUsers == null )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

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
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("Read")]
        public async Task<IActionResult> Read([FromBody] string userMail)
        {
            //--- Check Input Data
            var foundedUser = await _userManager.FindByEmailAsync(userMail);

            // User cannot found Admin user
            if ( foundedUser == null || foundedUser?.Role == UserRoles.Admin )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

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
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(string userMail, [FromBody] AuthRegisterModel inputUser)
        {
            //--- Check Input Data
            var foundedUser = await _userManager.FindByEmailAsync(userMail);

            if ( foundedUser == null )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            //--- Check Input Data
            var userRole = inputUser.Role;
            if ( userRole == null )
                return BadRequest(WebResponcesAuth.authResponceErrorRole);

            if ( !await _roleManager.RoleExistsAsync(userRole) )
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponcesAuth.authResponceErrorNoRoleInDb);

            // User cannot found Admin mail
            if ( userRole == UserRoles.Admin )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            //--- Updating data of user

            foundedUser.Email = inputUser.Email;
            foundedUser.FullUserName = inputUser.FullUserName;
            foundedUser.Role = userRole;
            foundedUser.PhoneNumber = inputUser.PhoneNumber;
            foundedUser.Password = inputUser.Password;
            foundedUser.UserName = inputUser.Email;

            var result = await _userManager.UpdateAsync(foundedUser);

            if ( !result.Succeeded )
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponcesAuth.authResponceErrorUserUpdate);

            return Ok(WebResponcesAuth.authResponceSuccessUserUpdate);
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
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] string userMail)
        {
            //--- Check Input Data
            var foundedUser = await _userManager.FindByEmailAsync(userMail);

            // User cannot found Admin user
            if ( foundedUser == null )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            if ( foundedUser.Role == UserRoles.Admin )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            var result = await _userManager.DeleteAsync(foundedUser);

            if ( !result.Succeeded )
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponcesAuth.authResponceSuccessUserDelete);

            return Ok(WebResponcesAuth.authResponceSuccessUserDelete);
        }

        #endregion Delete
    }
}