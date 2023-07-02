namespace DatabaseMonitoring.Services.Workspace.ApplicationEvents.Events;

/// <summary>
/// This event represents situation when server was added to workspaces
/// </summary>
public record ServerAddedToWorkspaceAppEvent : BaseEvent
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