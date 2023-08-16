namespace DatabaseMonitoring.Services.Notification.WebApi.ViewModels;

public class MarkNotificationAsReadRequest
{   
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public IEnumerable<string> NotificationsId { get; set; }
}