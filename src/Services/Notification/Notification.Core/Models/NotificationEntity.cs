namespace DatabaseMonitoring.Services.Notification.Core.Models;

[BsonCollection("Notifications")]
public class NotificationEntity : BaseEntity
{
    public string Data { get; set; }
    public ICollection<Guid> Receivers { get; set; }
    public ICollection<Guid> WorkspacesId { get; set; }
}