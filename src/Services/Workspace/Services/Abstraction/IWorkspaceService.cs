namespace DatabaseMonitoring.Services.Workspace.Services.Abstraction;

/// <summary>
/// Interface that contains required logic in order to work with workspaces
/// </summary>
public interface IWorkspaceService
{
    /// <summary>
    /// Check if workspace exists by id asynchronous
    /// </summary>
    /// <param name="id">Workspace identifier</param>
    /// <returns>True if exists, otherwise false</returns>
    Task<bool> WorkspaceExists(Guid id);

    /// <summary>
    /// Get workspace DTO by id asynchronous
    /// </summary>
    /// <param name="id">Workspace identifier</param>
    /// <returns><see cref="WorkspaceDto"/> or null</returns>
    Task<WorkspaceDto> GetWorkspaceByIdAsync(Guid id);

    /// <summary>
    /// Get all workspace DTOs 
    /// </summary>
    /// <returns>Array of workspaces</returns>
    Task<IEnumerable<WorkspaceDto>> GetAllWorkspacesAsync();

    /// <summary>
    /// Add new workspace to database asynchronous
    /// </summary>
    /// <param name="workspaceDto">Worksapce DTO</param>
    /// <returns>Generated identifier</returns>
    Task<Guid> CreateWorkspaceAsync(WorkspaceDto workspaceDto);

    /// <summary>
    /// Delete workspace from databasy by id asynchronous
    /// </summary>
    /// <param name="id">Workspace identifier</param>
    /// <returns>True if entity was deleted successfully, otherwise false</returns>
    Task<bool> DeleteWorkspaceAsync(Guid id);

    /// <summary>
    /// Update workspace asynchronous
    /// </summary>
    /// <param name="id">Workspace identifier</param>
    /// <param name="workspaceDto">Updated worksapce DTO</param>
    /// <returns>True if update was successfull, otherwise false</returns>
    Task<bool> UpdateWorkspaceAsync(Guid id, WorkspaceDto workspaceDto);

    /// <summary>
    /// Get wokrspace server asynchronous
    /// </summary>
    /// <param name="id">Workspace identifier</param>
    /// <returns>Array of servers identifiers</returns>
    Task<IEnumerable<Guid>> GetWorkspaceServersAsync(Guid id);

    /// <summary>
    /// Get wokrspace users asynchronous
    /// </summary>
    /// <param name="id">Workspace identifier</param>
    /// <returns>Array of users identifiers</returns>
    Task<IEnumerable<Guid>> GetWorkspaceUsersAsync(Guid id);

    /// <summary>
    /// Add user to workspace asynchronous
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="workspaceId">Workspace identifier</param>
    Task AddUserToWorkspaceAsync(Guid workspaceId, Guid userId);

    /// <summary>
    /// Add server to workspace asynchronous
    /// </summary>
    /// <param name="serverId">Server identifier</param>
    /// <param name="workspaceId">Workspace identifier</param>
    Task AddServerToWorkspaceAsync(Guid workspaceId, Guid serverId);

    /// <summary>
    /// Remove server from workspace asynchronous
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="workspaceId">Workspace identifier</param>
    Task<bool> RemoveUserFromWorkspaceAsync(Guid workspaceId, Guid userId);

    /// <summary>
    /// Remove server from workspace asynchronous
    /// </summary>
    /// <param name="serverId">Server identifier</param>
    /// <param name="workspaceId">Workspace identifier</param>
    Task<bool> RemoveServerFromWorkspaceAsync(Guid workspaceId, Guid serverId);

    /// <summary>
    /// Get all users associated with server
    /// </summary>
    /// <param name="serverId">Server identifier</param>
    /// <returns>Array of users identifiers</returns>
    Task<IEnumerable<Guid>> GetUsersAssociatedWithServer(Guid serverId);
}