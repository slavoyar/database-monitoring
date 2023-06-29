namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.EventHandling;

public class UserRemovedFromWorkspaceAppEventHandler : WorkspaceAppEventsHandler, IBaseEventHandler<UserRemovedFromWorkspaceAppEvent>
{
    public UserRemovedFromWorkspaceAppEventHandler(
        INotificationService notificationService,
        ILogger<WorkspaceAppEventsHandler> logger
        ) : base(notificationService, logger)
    {     
    }
    public async Task Handle(UserRemovedFromWorkspaceAppEvent @event)
    {
        await HandleWorkspaceEvent(@event);
    }
}