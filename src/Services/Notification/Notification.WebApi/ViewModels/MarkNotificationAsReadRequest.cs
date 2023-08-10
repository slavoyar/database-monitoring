namespace DatabaseMonitoring.Services.Notification.WebApi.ViewModels;

public class MarkNotificationAsReadRequest
{   
    [Required]
    public Guid UserId { get; set; }
    public IEnumerable<string> NotificationsId { get; set; }
}