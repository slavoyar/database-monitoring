namespace DatabaseMonitoring.Services.Workspace.Controllers;

/// <summary>
/// Web api controller for working with workspaces
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = "Admin")]
public class WorkspaceController : ControllerBase
{
    private readonly ILogger<WorkspaceController> logger;
    private readonly IWorkspaceService workspaceService;
    private readonly IMapper mapper;

    /// <summary>
    /// Contructor
    /// </summary>
    public WorkspaceController(
        ILogger<WorkspaceController> logger,
        IWorkspaceService workspaceService,
        IMapper mapper
        )
    {
        this.logger = logger;
        this.workspaceService = workspaceService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get workspace by id
    /// </summary>
    /// <param name="workspaceId">Workspace identifier</param>
    [HttpGet("{workspaceId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetWorkspaceResponse>> GetWorkspaceAsync(Guid workspaceId)
    {
        if (!await workspaceService.WorkspaceExists(workspaceId))
            return NotFound(ConstantResponseMessages.NoWorkspaceWasFound);

        var resultEntity = await workspaceService.GetWorkspaceByIdAsync(workspaceId);
        return mapper.Map<GetWorkspaceResponse>(resultEntity);
    }

    /// <summary>
    /// Get all workspaces
    /// </summary>
    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetWorkspaceResponse>>> GetAllWorkspacesAsync()
    {
        var workspaces = await workspaceService.GetAllWorkspacesAsync();
        return workspaces.Select(w => mapper.Map<GetWorkspaceResponse>(w)).ToList();
    }

    /// <summary>
    /// Create new workspace
    /// </summary>
    /// <param name="request">New workspace data</param>
    /// <returns>Generated identifier</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateWorkspaceAsync(UpsertWorkspaceRequest request)
    {
        var workspaceDto = mapper.Map<WorkspaceDto>(request);
        var id = await workspaceService.CreateWorkspaceAsync(workspaceDto);
        return Ok(id);
    }


    /// <summary>
    /// Delete workspace by id 
    /// </summary>
    /// <param name="workspaceId">Workspace identifier</param>
    /// <returns></returns>
    [HttpDelete("{workspaceId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteWorkspaceAsync(Guid workspaceId)
    {
        if (await workspaceService.DeleteWorkspaceAsync(workspaceId))
            return Ok();
        else
            return NotFound(ConstantResponseMessages.NoWorkspaceWasFound);
    }

    /// <summary>
    /// Update workspace by id
    /// </summary>
    /// <param name="workspaceId">Workspace identifier</param>
    /// <param name="request">New workspace data</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{workspaceId}")]
    public async Task<ActionResult> UpdateWorksapceByIdAsync(Guid workspaceId, UpsertWorkspaceRequest request)
    {
        if (await workspaceService.WorkspaceExists(workspaceId) == false)
            return NotFound(ConstantResponseMessages.NoWorkspaceWasFound);

        var workspaceDto = mapper.Map<WorkspaceDto>(request);

        if (await workspaceService.UpdateWorkspaceAsync(workspaceId, workspaceDto))
            return Ok();
        else
            return StatusCode(500);
    }

    /// <summary>
    /// Get all users associated with server
    /// </summary>
    /// <param name="serverId">Server identifier</param>
    /// <returns>Array of users identifier</returns>
    [HttpGet("UsersByServerId/{serverId}")]
    public async Task<ActionResult<IEnumerable<Guid>>> GetUsersAssociatedWithServer(Guid serverId)
    {
        return Ok(await workspaceService.GetUsersAssociatedWithServer(serverId));
    }
}
