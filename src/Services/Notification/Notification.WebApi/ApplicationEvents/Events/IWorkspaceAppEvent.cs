namespace DatabaseMonitoring.Services.Notification.WebApi.ApplicationEvents.Events;

public interface IWorkspaceAppEvent
{
    Guid Id {get; }
    IEnumerable<Guid> UsersId { get; }
    Guid WorkspaceId { get; }
    DateTime CreationDate{ get; }
}