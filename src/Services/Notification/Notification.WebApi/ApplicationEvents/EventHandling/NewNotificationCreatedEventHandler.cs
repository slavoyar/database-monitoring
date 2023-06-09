namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.EventHandling;

public class NewNotificationCreatedEventHandler : IBaseEventHandler<NewNotificationCreatedEvent>
{
    private readonly ILogger<NewNotificationCreatedEventHandler> logger;
    private readonly INotificationService notificationService;
    private readonly IWorkspaceService workspaceService;

    public NewNotificationCreatedEventHandler(
        ILogger<NewNotificationCreatedEventHandler> logger,
        INotificationService notificationService,
        IWorkspaceService workspaceService)
    {
        this.logger = logger;
        this.notificationService = notificationService;
        this.workspaceService = workspaceService;
    }
    public async Task Handle(NewNotificationCreatedEvent @event)
    {
        using (logger.BeginScope(new List<KeyValuePair<string, object>>{new ("BaseEventContext", @event.Id)}))
        {
            if(@event.WorkspaceId == null)
                await HandleWithoutWorkspaceId(@event);
            else
                await HandleWithWorkspaceId(@event);
        }
    }

    private async Task HandleWithoutWorkspaceId(NewNotificationCreatedEvent @event)
    {
        logger.LogInformation("Handling event without workspace id: {BaseEvent} - ({@BaseEvent})", @event.Id, @event);

        var usersId = await workspaceService.GetUsersAssociatedWithServer((Guid)@event.ServerId);
        var WorkspacesId = await workspaceService.GetWorkspacesAssociatedWithServer((Guid)@event.ServerId);

        var newNotificationDto = new NewNotificationDto();
        newNotificationDto.CreationDate = @event.CreationDate;
        newNotificationDto.Data = @event.Data;
        newNotificationDto.UsersId = usersId.ToList();
        newNotificationDto.WorkspacesId = WorkspacesId.ToList();

        await notificationService.CreateNewNotification(newNotificationDto);
    }

    private async Task HandleWithWorkspaceId(NewNotificationCreatedEvent @event)
    {
        logger.LogInformation("Handling event with workspace id: {BaseEvent} - ({@BaseEvent})", @event.Id, @event);

        var usersId = await workspaceService.GetUsersAssociatedWithWorkspace((Guid)@event.WorkspaceId);

        var newNotificationDto = new NewNotificationDto();
        newNotificationDto.CreationDate = @event.CreationDate;
        newNotificationDto.Data = @event.Data;
        newNotificationDto.UsersId = usersId.ToList();
        newNotificationDto.WorkspacesId = new List<Guid>{(Guid)@event.WorkspaceId};

        await notificationService.CreateNewNotification(newNotificationDto);
    }
}