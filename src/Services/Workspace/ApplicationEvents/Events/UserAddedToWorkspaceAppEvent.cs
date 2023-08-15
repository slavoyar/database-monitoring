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

    /// <summary>
    /// Map from models
    /// </summary>
    /// <param name="user">User that was added</param>
    /// <param name="workspace">Workspace, where user was added user</param>
    /// <returns></returns>
    public static UserAddedToWorkspaceAppEvent FromModels(User user, WorkspaceEntity workspace)
        => new UserAddedToWorkspaceAppEvent{UserId = user.OuterId, UsersId = workspace.Users.Select(u => u.OuterId), WorkspaceId = workspace.Id};
}