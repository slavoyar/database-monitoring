
namespace DatabaseMonitoring.Services.Notification.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> logger;
    private readonly IMailService mailsService;
    private readonly IMapper mapper;
    private readonly IWorkspaceService workspaceService;

    public EmailController(
        ILogger<EmailController> logger,
        IMailService mailsService,
        IMapper mapper,
        IWorkspaceService workspaceService)
    {
        this.logger = logger;
        this.mailsService = mailsService;
        this.mapper = mapper;
        this.workspaceService = workspaceService;
    }

    [HttpPost]
    [Route("Send/{workspaceId}")]
    public async Task<ActionResult> SendSingleEmailAsync(Guid workspaceId)//SendEmailRequest request)
    {
        var result = await workspaceService.GetWorkspaceUsers(workspaceId);
        return Ok(result);

        // var mailData = mapper.Map<MailData>(request);
        // var cts = new CancellationToken();
        // await mailsService.SendAsync(mailData, cts);
        // return Ok();
    }
}