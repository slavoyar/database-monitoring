namespace DatabaseMonitoring.Services.Notification.Infrastructure.Services.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetUnreadNotifications(Guid userId, Guid workspaceId);
    Task MarkAsRead(Guid userId, IEnumerable<string> notificationsId);
    Task<string> CreateNewNotificationAsync(NotificationDto notificationDto);
}