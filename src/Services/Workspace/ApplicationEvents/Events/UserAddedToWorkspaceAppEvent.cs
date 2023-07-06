namespace DatabaseMonitoring.Services.Workspace.ApplicationEvents.Events;


/// <summary>
/// This event represents situation when users was added to workspaces
/// </summary>
public record UserAddedToWorkspaceAppEvent : BaseEvent
{
    /// <summary>
    /// User identifer
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Identifiers of all users in affected workspace
    /// </summary>
    public IEnumerable<Guid> UsersId { get; set; }

    /// <summary>
    /// Workspace identifier
    /// </summary>
    public Guid WorkspaceId { get; init; }
}