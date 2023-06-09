namespace DatabaseMonitoring.Services.Notification.WebApi.Services;

public interface IWorkspaceService
{
    Task<IEnumerable<Guid>> GetUsersAssociatedWithServer(Guid serverId);
    Task<IEnumerable<Guid>> GetWorkspacesAssociatedWithServer(Guid serverId);
    Task<IEnumerable<Guid>> GetUsersAssociatedWithWorkspace(Guid workspaceId);
}