namespace DatabaseMonitoring.Services.Notification.WebApi.Services;

public interface IWorkspaceService
{
    Task<Guid> GetServerWorkspaces(Guid serverId);
}