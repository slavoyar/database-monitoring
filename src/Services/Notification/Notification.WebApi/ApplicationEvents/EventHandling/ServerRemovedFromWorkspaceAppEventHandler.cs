namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.EventHandling;

public class ServerRemovedFromWorkspaceAppEventHandler : WorkspaceAppEventsHandler, IBaseEventHandler<ServerRemovedFromWorkspaceAppEvent>
{
    public ServerRemovedFromWorkspaceAppEventHandler(
        INotificationService notificationService,
        ILogger<WorkspaceAppEventsHandler> logger
        ) : base(notificationService, logger)
    {     
    }
    public async Task Handle(ServerRemovedFromWorkspaceAppEvent @event)
    {
        await HandleWorkspaceEvent(@event);
    }
}