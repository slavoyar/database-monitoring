namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.EventHandling;

public class WorkspaceAppEventsHandler 
{
    private readonly INotificationService notificationService;
    private readonly ILogger<WorkspaceAppEventsHandler> logger;

    public WorkspaceAppEventsHandler(
        INotificationService notificationService,
        ILogger<WorkspaceAppEventsHandler> logger
        )
    {
        this.notificationService = notificationService;
        this.logger = logger;
    }
    protected async Task HandleWorkspaceEvent(IWorkspaceAppEvent @event)
    {
        using (logger.BeginScope(new List<KeyValuePair<string,object>>{new ("ApplicationEventContext", @event.Id)}))
        {
            logger.LogInformation("Handling application event: {ApplicationEventId} - ({@ApplicationEvent})", @event.Id, @event);
            
            var data = @event.GetType().Name switch 
            {
                nameof(ServerAddedToWorkspaceAppEvent) => ConstantMessages.NewServerAddedToWorkspace,
                nameof(ServerRemovedFromWorkspaceAppEvent) => ConstantMessages.ServerWasDeletedFromWokrspace,
                nameof(UserAddedToWorkspaceAppEvent) => ConstantMessages.NewUserAddedToWorkspaace,
                nameof(UserRemovedFromWorkspaceAppEvent) => ConstantMessages.UserDeletedFromWorksapce,
                _ => throw new ArgumentException("Uknown application event was been tried to handle")
            };

            var notificationDto = new NotificationDto()
            {
                Data = data,
                Receivers = @event.UsersId.ToList(),
                WorkspacesId = new List<Guid>{@event.WorkspaceId},
                CreationDate = @event.CreationDate,
            };

            var id = await notificationService.CreateNewNotificationAsync(notificationDto);
            logger.LogInformation("New notifcaiton with id: {notificaitonId} was created", id);
        }
    }
}