namespace DatabaseMonitoring.Services.Notification.WebApi.Services;

public class WorkspaceService : IWorkspaceService
{
    public Task<IEnumerable<Guid>> GetUsersAssociatedWithServer(Guid serverId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Guid>> GetUsersAssociatedWithWorkspace(Guid workspaceId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Guid>> GetWorkspacesAssociatedWithServer(Guid serverId)
    {
        throw new NotImplementedException();
    }
}