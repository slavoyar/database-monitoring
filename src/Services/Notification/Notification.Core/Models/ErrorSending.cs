namespace DatabaseMonitoring.Services.Notification.Core.Models;

public class ErrorSending : BaseEntity
{
    [Required]
    public string Reason { get; set; }
    [Required]
    public MailEntity MailEntity { get; set; }
}