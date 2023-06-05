namespace DatabaseMonitoring.Services.Notification.Core.Models;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
}