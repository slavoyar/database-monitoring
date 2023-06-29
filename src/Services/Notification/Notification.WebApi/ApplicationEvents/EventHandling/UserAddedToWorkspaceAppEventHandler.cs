namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.EventHandling;

public class UserAddedToWorkspaceAppEventHandler : WorkspaceAppEventsHandler, IBaseEventHandler<ServerAddedToWorkspaceAppEvent>
{
    public UserAddedToWorkspaceAppEventHandler(
        INotificationService notificationService,
        ILogger<WorkspaceAppEventsHandler> logger
        ) : base(notificationService, logger)
    {     
    }
    public async Task Handle(ServerAddedToWorkspaceAppEvent @event)
    {
        await HandleWorkspaceEvent(@event);
    }
}