namespace DatabaseMonitoring.Services.Workspace.Controllers;


/// <summary>
/// Web api controller for working with servers in workspace
/// </summary>
[ApiController]
[Route("api/v1/workspace/{workspaceId}/servers")]
public class ServersController : ControllerBase
{
    private readonly ILogger<ServersController> logger;
    private readonly IWorkspaceService workspaceService;
    private readonly IMapper mapper;

    /// <summary>
    /// Contructor
    /// </summary>
    public ServersController(
        ILogger<ServersController> logger,
        IWorkspaceService workspaceService,
        IMapper mapper

    )
    {
        this.logger = logger;
        this.workspaceService = workspaceService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get servers identifiers by workspace id
    /// </summary>
    /// <param name="workspaceId">Workspace identifier</param>
    /// <returns>Array of server identifiers</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Guid>>> GetWorkspaceServersAsync(Guid workspaceId)
    {
        if(await workspaceService.WorkspaceExists(workspaceId) == false)
            return NotFound(ConstantResponceMessages.NoWorkspaceWasFound);

        var serverIds = await workspaceService.GetWorkspaceServersAsync(workspaceId);
        return Ok(serverIds);
    }

    /// <summary>
    /// Add server to worksapce
    /// </summary>
    /// <param name="serverId">Server indetifier</param>
    /// <param name="workspaceId">Workspace indetifier</param>
    /// <returns>Array of users identifiers</returns>
    [HttpPost("{serverId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddServerToWorkspaceAsync(Guid workspaceId, Guid serverId)
    {
        if(await workspaceService.WorkspaceExists(workspaceId) == false)
            return NotFound(ConstantResponceMessages.NoWorkspaceWasFound);

        await workspaceService.AddServerToWorkspaceAsync(workspaceId, serverId);
        return Ok();
    }

    /// <summary>
    /// Remove server from worksapce
    /// </summary>
    /// <param name="serverId">Server indetifier</param>
    /// <param name="workspaceId">Workspace indetifier</param>
    [HttpDelete("{serverId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveServerFromWorkspaceAsync(Guid workspaceId, Guid serverId)
    {
        if(await workspaceService.WorkspaceExists(workspaceId) == false)
            return NotFound(ConstantResponceMessages.NoWorkspaceWasFound);
        if(await workspaceService.RemoveServerFromWorkspaceAsync(workspaceId, serverId))
            return Ok();
        else
            return NotFound(ConstantResponceMessages.NoServerInWorkspaceWasFound);
    }
}