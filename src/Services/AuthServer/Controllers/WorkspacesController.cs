using Auth.Data;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Auth.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/Management/[controller]")]
    [ApiController]
    public class WorkspacesController : ControllerBase
    {
        #region Constructor and Common vars

        private readonly AuthDbContext _authWebApplicationDbContext;

        public WorkspacesController(AuthDbContext authWebApplicationDbContext)
        {
            _authWebApplicationDbContext = authWebApplicationDbContext;
        }

        #endregion Constructor and Common vars

        #region Create

        /// <summary>
        /// Create new workspace
        /// </summary>
        /// <param name="inputWorkspace">Model of data for Workspace</param>
        /// <returns></returns>
        /// <response code="200">Success creating</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] WorkspacesTeamsInitModel inputWorkspace)
        {
            //--- Check Input Data
            var foundedWorkspace = _authWebApplicationDbContext.Workspaces
                .Where(workspace => workspace.Name == inputWorkspace.Name)
                .ToList();

            if ( foundedWorkspace.Count != 0 )
                return BadRequest(WebResponcesWorkspaces.WebResponceErrorAlreadyExist);

            //--- Creating workspace
            var newWorkspace = new Workspaces()
            {
                WorkspacesId = Guid.NewGuid().ToString(),
                Description = inputWorkspace.Description,
                Name = inputWorkspace.Name
            };

            _authWebApplicationDbContext.Workspaces.Add(newWorkspace);
            await _authWebApplicationDbContext.SaveChangesAsync();

            return Ok(WebResponcesWorkspaces.WebResponceSuccessCreate);
        }

        #endregion Create

        #region Read

        /// <summary>
        /// Get workspace with input name
        /// </summary>
        /// <param name="workspaceName"></param>
        /// <returns></returns>
        /// <response code="200">Success reading</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("Read")]
        public async Task<IActionResult> Read([FromBody] string workspaceName)
        {
            //--- Check Input Data
            var foundedWorkspace = _authWebApplicationDbContext.Workspaces
                .Include(x => x.Users)
                .Where(workspace => workspace.Name == workspaceName)
                .FirstOrDefault();

            if ( foundedWorkspace is null )
                return BadRequest(WebResponcesWorkspaces.WebResponceErrorNotFound);

            //--- Return found Workspaces
            return Ok(foundedWorkspace);
        }

        #endregion Read

        #region Update

        /// <summary>
        /// Update workspace with input name
        /// </summary>
        /// <param name="workspaceName">Name of workspace to update</param>
        /// <param name="inputWorkspace">New model data for updating workspace</param>
        /// <returns></returns>
        /// <response code="200">Success updating</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(string workspaceName, [FromBody] WorkspacesTeamsInitModel inputWorkspace)
        {
            //--- Check Input Data
            var foundedWorkspace = _authWebApplicationDbContext.Workspaces
                .Where(workspace => workspace.Name == workspaceName)
                .FirstOrDefault();

            if ( foundedWorkspace is null )
                return BadRequest(WebResponcesWorkspaces.WebResponceErrorNotFound);

            //--- Updating data of workspace
            foundedWorkspace.Name = inputWorkspace.Name;
            foundedWorkspace.Description = inputWorkspace.Description;

            _authWebApplicationDbContext.Workspaces.Update(foundedWorkspace);
            await _authWebApplicationDbContext.SaveChangesAsync();

            return Ok(WebResponcesWorkspaces.WebResponceSuccessUpdate);
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Delete workspace with input name
        /// </summary>
        /// <param name="workspaceName">Name of workspace to Delete</param>
        /// <returns></returns>
        /// <response code="200">Success deleting</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [ProducesResponseType(typeof(WebResponce), 200)]
        [ProducesResponseType(typeof(WebResponce), 400)]
        [ProducesResponseType(typeof(WebResponce), 401)]
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] string workspaceName)
        {
            //--- Check Input Data
            var foundedWorkspace = _authWebApplicationDbContext.Workspaces
                .Where(workspace => workspace.Name == workspaceName)
                .FirstOrDefault();

            if ( foundedWorkspace is null )
                return BadRequest(WebResponcesWorkspaces.WebResponceErrorNotFound);

            //--- Delete found workspace
            _authWebApplicationDbContext.Workspaces.Remove(foundedWorkspace);
            await _authWebApplicationDbContext.SaveChangesAsync();

            return Ok(WebResponcesWorkspaces.WebResponceSuccessDelete);
        }

        #endregion Delete
    }
}