namespace DatabaseMonitoring.Services.Notification.WebApi.Models.Requests;

public class SendEmailRequest
{
    [Required]
    public List<string> To { get; set; }
    [Required]
    public string Body { get; set; }
    [Required]
    public string Subject { get; set; }
}