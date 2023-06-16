namespace DatabaseMonitoring.Services.Workspace.ApplicationEvents.Events;

/// <summary>
/// This event represents situation when server was removed to workspaces
/// </summary>
public record ServerRemovedFromWorkspaceAppEvent : BaseEvent
{
    /// <summary>
    /// Server identifer
    /// </summary>
    public Guid ServerId { get; init; }

    /// <summary>
    /// Workspace identifier
    /// </summary>
    public Guid WorkspaceId { get; init; }
}