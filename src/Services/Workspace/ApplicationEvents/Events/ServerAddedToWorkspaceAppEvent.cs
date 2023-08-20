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

    /// <summary>
    /// Map from models
    /// </summary>
    /// <param name="server">Server that was added</param>
    /// <param name="workspace">Workspace, where server was added</param>
    /// <returns></returns>
    public static ServerAddedToWorkspaceAppEvent FromModels(Server server, WorkspaceEntity workspace)
        => new ServerAddedToWorkspaceAppEvent{ServerId = server.OuterId, UsersId = workspace.Users.Select(u => u.OuterId), WorkspaceId = workspace.Id};
}