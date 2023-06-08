namespace DatabaseMonitoring.BuildingBlocks.EventBus.Events;

///<summary>
///Record fow sending notifications to users in a workspace
///</summmary>
public record NewNotificationCreatedEvent : BaseEvent
{
    private NewNotificationCreatedEvent(Guid? workspaceId, Guid? serverId, string data)
    {
        WorkspaceId = workspaceId;
        ServerId = serverId;
        Data = data;
    }
    ///<summary>
    ///Create new instance with serverId and data
    ///</summmary>
    public static NewNotificationCreatedEvent WithServerId(Guid serverId, string data)
        => new NewNotificationCreatedEvent(null, serverId, data);
    ///<summary>
    ///Create new instance with workspaceId and data
    ///</summmary>
    public static NewNotificationCreatedEvent WithWorkspaceId(Guid workspaceId, string data)
        => new NewNotificationCreatedEvent(workspaceId, null, data);

    public string Data { get; set; }
    public Guid? WorkspaceId { get; }
    public Guid? ServerId { get; }
}
