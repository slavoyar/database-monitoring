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
    /// Identifiers of all users in affected workspace
    /// </summary>
    public IEnumerable<Guid> UsersId { get; set; }

    /// <summary>
    /// Workspace identifier
    /// </summary>
    public Guid WorkspaceId { get; init; }

    /// <summary>
    /// Map from models
    /// </summary>
    /// <param name="user">User that was deleted</param>
    /// <param name="workspace">Workspace, from which was deleted user</param>
    /// <returns></returns>
    public static UserRemovedFromWorkspaceAppEvent FromModels(User user, WorkspaceEntity workspace)
        => new UserRemovedFromWorkspaceAppEvent{UserId = user.OuterId, UsersId = workspace.Users.Select(u => u.OuterId), WorkspaceId = workspace.Id};
}