namespace DatabaseMonitoring.Services.Notification.WebApi.Services;

public interface IWorkspaceService
{
    Task<IEnumerable<Guid>> GetWorkspaceUsers(Guid workspaceId);

    Task<IEnumerable<Guid>> GetUsersAssociatedWithServer(Guid serverId);
}