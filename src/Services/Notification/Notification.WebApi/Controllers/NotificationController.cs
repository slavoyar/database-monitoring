namespace DatabaseMonitoring.Services.Notification.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly INotificationService notificationService;
    private readonly IMapper mapper;

    public NotificationController(INotificationService notificationService, IMapper mapper)
    {
        this.notificationService = notificationService;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnreadNotification>>> GetUnreadNotifications(Guid userid, Guid workspaceId)
    {
        var unreadNotifications = await notificationService.GetUnreadNotifications(userid, workspaceId);
        return Ok(mapper.Map<IEnumerable<UnreadNotification>>(unreadNotifications));
    }

    [HttpPost]
    public async Task<ActionResult> MarkNotificationsAsRead(Guid userId, IEnumerable<string> notificationsId)
    {
        await notificationService.MarkAsRead(userId, notificationsId);
        return Ok();
    }
}