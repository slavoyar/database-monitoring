namespace DatabaseMonitoring.Services.Notification.Core.Models;

public class MailEntity : BaseEntity
{
    [Required]
    public string Recepients { get; set; }
    [Required]
    public string Body { get; set; }
}