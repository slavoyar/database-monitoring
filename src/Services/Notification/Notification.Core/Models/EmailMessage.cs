namespace DatabaseMonitoring.Services.Notification.Core.Models;

public class EmailMessage : BaseEntity
{
    public string Recepient { get; set; }
    public string Body { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsSent { get; set; }
}