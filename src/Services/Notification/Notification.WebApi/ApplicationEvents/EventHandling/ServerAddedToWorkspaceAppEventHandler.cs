namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.EventHandling;

public class ServerAddedToWorkspaceAppEventHandler : WorkspaceAppEventsHandler, IBaseEventHandler<ServerAddedToWorkspaceAppEvent>
{
    public ServerAddedToWorkspaceAppEventHandler(
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