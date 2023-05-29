namespace DatabaseMonitoring.Services.Notification.Core.Interfaces;

public interface INotificationRepository : IRepository<NotificationEntity>
{
    Task<NotificationEntity> UpdateAsync(NotificationEntity entity);

}