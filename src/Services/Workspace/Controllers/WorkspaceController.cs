using DatabaseMonitoring.Services.Workspace.Infrustructure.Repository.Implementation;

namespace DatabaseMonitoring.Services.Workspace.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class WorkspaceController : ControllerBase
{
    private readonly ILogger<WorkspaceController> logger;
    private readonly IWorkspaceService workspaceService;
    private readonly IMapper mapper;

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

    ///<summary>
    ///Get workspace information
    ///</summmary>
    [HttpGet("{workspaceId}")]
    public async Task<ActionResult<GetWorkspaceResponce>> GetWorkspaceAsync(Guid workspaceId)
    {
        var resultEntity = await workspaceService.GetByIdAsync(workspaceId);
        return Ok(mapper.Map<GetWorkspaceResponce>(resultEntity));
    }

    /// <summary>
    /// Create new workspace
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Guid>> CreateWorkspaceAsync(CreateWorkspaceRequest request)
    {
        var workspaceDto = mapper.Map<WorkspaceDto>(request);
        var id = await workspaceService.CreateAsync(workspaceDto);
        return id;
    }


    /// <summary>
    /// Delete workspace by id
    /// </summmary>
    [HttpDelete("{workspaceId}")]
    public async Task<ActionResult> DeleteWorkspaceAsync(Guid workspaceId)
    {
        if(await workspaceService.DeleteAsync(workspaceId))
            return Ok();
        else
            return NotFound();

            var repo = new EfRepository<WorkspaceEntity>(default);
            repo.get
    }
}