namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.EventHandling;

public class ServerRemovedFromWorkspaceAppEventHandler : WorkspaceAppEventsHandler, IBaseEventHandler<ServerAddedToWorkspaceAppEvent>
{
    public ServerRemovedFromWorkspaceAppEventHandler(
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