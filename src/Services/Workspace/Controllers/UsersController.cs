namespace DatabaseMonitoring.Services.Workspace.Controllers;


/// <summary>
/// Web api controller for working with users in workspace
/// </summary>
[ApiController]
[Route("api/v1/workspace/{workspaceId}/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> logger;
    private readonly IWorkspaceService workspaceService;
    private readonly IMapper mapper;

    /// <summary>
    /// Contructor
    /// </summary>
    public UsersController(
        ILogger<UsersController> logger,
        IWorkspaceService workspaceService,
        IMapper mapper

    )
    {
        this.logger = logger;
        this.workspaceService = workspaceService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get users identifiers by workspace id
    /// </summary>
    /// <param name="workspaceId">Workspace identifier</param>
    /// <returns>Array of users identifiers</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Guid>>> GetWorkspaceUsersAsync(Guid workspaceId)
    {
        if(await workspaceService.WorkspaceExists(workspaceId) == false)
            return NotFound(ConstantResponceMessages.NoWorkspaceWasFound);
            
        var serverIds = await workspaceService.GetWorkspaceUsersAsync(workspaceId);
        return Ok(serverIds);
    }

    /// <summary>
    /// Add user to worksapce
    /// </summary>
    /// <param name="userId">User indetifier</param>
    /// <param name="workspaceId">Workspace indetifier</param>
    [HttpPost("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddUserToWorkspace(Guid workspaceId, Guid userId)
    {
        if(await workspaceService.WorkspaceExists(workspaceId) == false)
            return NotFound(ConstantResponceMessages.NoWorkspaceWasFound);
        await workspaceService.AddUserToWorkspaceAsync(workspaceId, userId);
        return Ok();
    }

    /// <summary>
    /// Remove user from worksapce
    /// </summary>
    /// <param name="userId">User indetifier</param>
    /// <param name="workspaceId">Workspace indetifier</param>
    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> RemoveUserFromWorkspaceAsync(Guid workspaceId, Guid userId)
    {
        if(await workspaceService.WorkspaceExists(workspaceId) == false)
            return NotFound(ConstantResponceMessages.NoWorkspaceWasFound);
        if(await workspaceService.RemoveUserFromWorkspaceAsync(workspaceId, userId))
            return Ok();
        else
            return NotFound(ConstantResponceMessages.NoUserInWorkspaceWasFound);
    }
}