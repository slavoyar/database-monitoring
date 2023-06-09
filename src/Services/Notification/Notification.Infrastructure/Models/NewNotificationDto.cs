namespace DatabaseMonitoring.Services.Notification.Infrastructure.Models;

public class NewNotificationDto
{
    public string Data { get; set; }
    public ICollection<Guid> UsersId { get; set; }
    public ICollection<Guid> WorkspacesId { get; set; }
    public DateTime CreationDate { get; set; }

    public static NotificationEntity MapToNotificationEntity(NewNotificationDto newNotification)
        => new NotificationEntity()
        {
            Data = newNotification.Data, 
            UsersReceived = newNotification.UsersId, 
            WorkspacesId = newNotification.WorkspacesId,
            CreationDate = newNotification.CreationDate
            };
}