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
    /// <param name="server">Server that was removed</param>
    /// <param name="workspace">Workspace, from which server was removed</param>
    /// <returns></returns>
    public static ServerRemovedFromWorkspaceAppEvent FromModels(Server server, WorkspaceEntity workspace)
        => new ServerRemovedFromWorkspaceAppEvent{ServerId = server.OuterId, UsersId = workspace.Users.Select(u => u.OuterId), WorkspaceId = workspace.Id};
}