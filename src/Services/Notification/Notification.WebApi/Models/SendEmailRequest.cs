using System.ComponentModel.DataAnnotations;

namespace DatabaseMonitoring.Services.Notification.WebApi.Models;

public class SendEmailRequest
{
    [Required]
    public string Recepient { get; set; }
    [Required]
    public string Body { get; set; }
    [Required]
    public string Subject { get; set; }
}