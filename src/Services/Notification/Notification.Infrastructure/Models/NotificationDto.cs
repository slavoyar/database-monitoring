namespace DatabaseMonitoring.Services.Notification.Infrastructure.Models;

public class NotificationDto
{
    public string Id { get; set; }
    public string Data { get; set; }
    public DateTime CreationDate { get; set; }
    public ICollection<Guid> Receivers { get; set; }
    public ICollection<Guid> WorkspacesId { get; set; }

    public static NotificationDto MapFrom(NotificationEntity entity)
        => new NotificationDto { Id = entity.Id, Data = entity.Data, CreationDate = entity.CreationDate, Receivers = entity.Receivers, WorkspacesId = entity.WorkspacesId };

    public static NotificationEntity MapToNotificationEntity(NotificationDto notificationDto)
        => new NotificationEntity
            {
                Data = notificationDto.Data, 
                CreationDate = notificationDto.CreationDate, 
                Receivers = notificationDto.Receivers, 
                WorkspacesId = notificationDto.WorkspacesId
            };
}