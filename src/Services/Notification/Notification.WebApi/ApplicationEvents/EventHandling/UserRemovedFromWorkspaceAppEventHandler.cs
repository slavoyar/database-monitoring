namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.EventHandling;

public class UserRemovedFromWorkspaceAppEventHandler : WorkspaceAppEventsHandler, IBaseEventHandler<ServerAddedToWorkspaceAppEvent>
{
    public UserRemovedFromWorkspaceAppEventHandler(
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