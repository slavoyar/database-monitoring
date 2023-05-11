namespace DatabaseMonitoring.Services.Notification.Core.Models;

public class BaseEntity
{
    public Guid Id { get; set; } = new Guid();
    public DateTime CreationDate { get; set; } = DateTime.Now;
}