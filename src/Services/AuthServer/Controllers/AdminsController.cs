using Auth.Data;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        #region Constructor and Common vars

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AuthUser> _userManager;
        private readonly AuthDbContext _authWebApplicationDbContext;

        public AdminsController(
            UserManager<AuthUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AuthDbContext authWebApplicationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authWebApplicationDbContext = authWebApplicationDbContext;
        }

        #endregion Constructor and Common vars

        #region Create

        /// <summary>
        /// Register User with input data as Admin (Only for Admins)
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
            var modelEmail = model.Email;
            if ( modelEmail == null )
                return BadRequest(WebResponcesAuth.authResponceErrorEmail);

            var modelPassword = model.Password;
            if ( modelPassword == null )
                return BadRequest(WebResponcesAuth.authResponceErrorPassword);

            var userExists = await _userManager.FindByEmailAsync(modelEmail);
            if ( userExists != null )
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponcesAuth.authResponceErrorUserExist);

            //--- Create User + Add Role to User
            AuthUser user = new()
            {
                Email = model.Email,
                FullUserName = model.FullUserName,
                Role = model.Role,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, modelPassword);

            if ( !result.Succeeded )
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponcesAuth.authResponceErrorUserCreate);

            //--- Add Role to User
            await AddRoleToUser(user, UserRoles.Admin);
            await AddRoleToUser(user, UserRoles.Engineer);
            await AddRoleToUser(user, UserRoles.Manager);

            return Ok(WebResponcesAuth.authResponceSuccessUserCreate);
        }

        #endregion Create

        #region Read

        /// <summary>
        /// Get all admins
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

            if ( foundedUser == null )
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

            //--- Updating data of user

            foundedUser.Email = inputUser.Email;
            foundedUser.FullUserName = inputUser.FullUserName;
            foundedUser.Role = userRole;
            foundedUser.PhoneNumber = inputUser.PhoneNumber;
            foundedUser.Password = inputUser.Password;
            foundedUser.UserName = inputUser.Email;

            var result = await _userManager.UpdateAsync(foundedUser);

            return Ok(WebResponcesAuth.authResponceErrorUserUpdate);
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

            if ( foundedUser == null )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            var result = await _userManager.DeleteAsync(foundedUser);

            if ( !result.Succeeded )
                return StatusCode(StatusCodes.Status500InternalServerError, WebResponcesAuth.authResponceSuccessUserDelete);

            return Ok(WebResponcesAuth.authResponceSuccessUserDelete);
        }

        #endregion Delete

        #region AddToWorkspace

        /// <summary>
        /// Add user to workspace
        /// </summary>
        /// <param name="workspaceName">Name of workspace to add</param>
        /// <param name="userMail">User Mail</param>
        /// <returns></returns>
        /// <response code="200">Success updating</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("AddToWorkspace")]
        public async Task<IActionResult> AddToWorkspace(string userMail, string workspaceName)
        {
            //--- Check Input Data
            var foundedWorkspace = _authWebApplicationDbContext.Workspaces
                .Include(x => x.Users)
                .Where(workspace => workspace.Name == workspaceName)
                .FirstOrDefault();

            if ( foundedWorkspace is null )
                return BadRequest(WebResponcesWorkspaces.WebResponceErrorNotFound);

            var foundedUser = await _userManager.FindByEmailAsync(userMail);

            // User cannot found Admin user
            if ( foundedUser == null )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            if ( foundedUser.Role == UserRoles.Admin )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            var checkWorkspace = foundedWorkspace
                .Users
                .Where(user => user.Email == userMail)
                .FirstOrDefault();

            if ( checkWorkspace != null )
                return BadRequest(WebResponcesAuth.authResponceErrorUserAddToWorkspace);

            foundedWorkspace.Users.Add(foundedUser);

            _authWebApplicationDbContext.Workspaces.Update(foundedWorkspace);
            await _authWebApplicationDbContext.SaveChangesAsync();

            return Ok(WebResponcesAuth.authResponceSuccessUserAddToWorkspace);
        }

        #endregion AddToWorkspace

        #region RemoveFromWorkspace

        /// <summary>
        /// Remove user from Workspace
        /// </summary>
        /// <param name="workspaceName">Name of Workspace to add</param>
        /// <param name="userMail">User Mail</param>
        /// <returns></returns>
        /// <response code="200">Success updating</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("RemoveFromWorkspace")]
        public async Task<IActionResult> RemoveFromWorkspace(string userMail, string workspaceName)
        {
            //--- Check Input Data
            var foundedWorkspace = _authWebApplicationDbContext.Workspaces
                .Include(x => x.Users)
                .Where(workspace => workspace.Name == workspaceName)
                .FirstOrDefault();

            if ( foundedWorkspace is null )
                return BadRequest(WebResponcesWorkspaces.WebResponceErrorNotFound);

            var foundedUser = await _userManager.FindByEmailAsync(userMail);

            // User cannot found Admin user
            if ( foundedUser == null )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            if ( foundedUser.Role == UserRoles.Admin )
                return BadRequest(WebResponcesAuth.authResponceErrorUser);

            var checkWorkspace = foundedWorkspace
                .Users
                .Where(user => user.Email == userMail)
                .FirstOrDefault();

            if ( checkWorkspace == null )
                return BadRequest(WebResponcesAuth.authResponceErrorUserRemoveFromWorkspace);

            foundedWorkspace.Users.Remove(foundedUser);

            _authWebApplicationDbContext.Workspaces.Update(foundedWorkspace);
            await _authWebApplicationDbContext.SaveChangesAsync();

            return Ok(WebResponcesAuth.authResponceSuccessUserRemoveFromWorkspace);
        }

        #endregion RemoveFromWorkspace

        #region Usefull Methods

        /// <summary>
        /// if role exists in database -> add role to user
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="userRole">role definition</param>
        /// <returns></returns>
        private async Task AddRoleToUser(AuthUser user, string userRole)
        {
            if ( await _roleManager.RoleExistsAsync(userRole) )
                await _userManager.AddToRoleAsync(user, userRole);
        }

        #endregion Usefull Methods
    }
}