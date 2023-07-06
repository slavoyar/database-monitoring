namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.EventHandling;

public class UserAddedToWorkspaceAppEventHandler : WorkspaceAppEventsHandler, IBaseEventHandler<UserAddedToWorkspaceAppEvent>
{
    public UserAddedToWorkspaceAppEventHandler(
        INotificationService notificationService,
        ILogger<WorkspaceAppEventsHandler> logger
        ) : base(notificationService, logger)
    {     
    }
    public async Task Handle(UserAddedToWorkspaceAppEvent @event)
    {
        await HandleWorkspaceEvent(@event);
    }
}