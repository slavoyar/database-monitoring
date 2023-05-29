namespace DatabaseMonitoring.Services.Notification.Infrastructure.Services.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetUnreadNotificationsForUserInWorkspace(Guid userId, Guid workspaceId);
    Task MarkAsRead(Guid userId, IEnumerable<string> notificationsId);

}