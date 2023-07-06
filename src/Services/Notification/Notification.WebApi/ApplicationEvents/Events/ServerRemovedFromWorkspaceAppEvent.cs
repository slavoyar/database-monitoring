namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.Events;

/// <summary>
/// This event represents situation when server was removed to workspaces
/// </summary>
public record ServerRemovedFromWorkspaceAppEvent : BaseEvent, IWorkspaceAppEvent
{
    /// <summary>
    /// Server identifer
    /// </summary>
    public Guid ServerId { get; init; }

    /// <summary>
    /// Identifiers of all users in affected workspace
    /// </summary>
    public IEnumerable<Guid> UsersId { get; set; }
    
    /// <summary>
    /// Workspace identifier
    /// </summary>
    public Guid WorkspaceId { get; init; }
}