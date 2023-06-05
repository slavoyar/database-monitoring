namespace DatabaseMonitoring.Services.Notification.Infrastructure.Models;

public class NotificationDto
{
    public string Id { get; set; }
    public string Data { get; set; }
    public DateTime CreationDate { get; set; }

    public static NotificationDto MapFrom(NotificationEntity entity)
        => new NotificationDto { Id = entity.Id, Data = entity.Data, CreationDate = entity.CreationDate };
}