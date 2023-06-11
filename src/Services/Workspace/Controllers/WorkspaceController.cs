using DatabaseMonitoring.Services.Workspace.Infrustructure.Repository.Implementation;

namespace DatabaseMonitoring.Services.Workspace.Controllers;

/// <summary>
/// Web api controller for working with workspaces
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
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
    /// <param name="workspaceId">Worksapce identifier</param>
    [HttpGet("{workspaceId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetWorkspaceResponce>> GetWorkspaceAsync(Guid workspaceId)
    {
        var resultEntity = await workspaceService.GetByIdAsync(workspaceId);
        if(resultEntity != null)
            return Ok(mapper.Map<GetWorkspaceResponce>(resultEntity));
        else
            return NotFound(ConstantResponceMessages.NoWorkspaceWasFound);
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
        var id = await workspaceService.CreateAsync(workspaceDto);
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
        if(await workspaceService.DeleteAsync(workspaceId))
            return Ok();
        else
            return NotFound(ConstantResponceMessages.NoWorkspaceWasFound);
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
        if(await workspaceService.WorkspaceExists(workspaceId) == false)
            return NotFound(ConstantResponceMessages.NoWorkspaceWasFound);

        var workspaceDto = mapper.Map<WorkspaceDto>(request);
        
        if(await workspaceService.UpdateAsync(workspaceId, workspaceDto))
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