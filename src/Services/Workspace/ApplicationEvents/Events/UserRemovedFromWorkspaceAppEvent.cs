namespace DatabaseMonitoring.Services.Workspace.ApplicationEvents.Events;

/// <summary>
/// This event represents situation when user was removed to workspaces
/// </summary>
public record UserRemovedFromWorkspaceAppEvent : BaseEvent
{
    /// <summary>
    /// User id identifer
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Workspace identifier
    /// </summary>
    public Guid WorkspaceId { get; init; }
}