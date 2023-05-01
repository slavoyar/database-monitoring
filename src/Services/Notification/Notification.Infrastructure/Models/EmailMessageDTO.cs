namespace DatabaseMonitoring.Services.Notification.Infrastructure.Models;
public class EmailMessageDTO
{
    public Guid Id { get; set; }
    public string Recepient { get; set; }
    public string Body { get; set; }
}