namespace DatabaseMonitoring.Services.Notification.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository repository;

    public NotificationService(INotificationRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<NotificationDto>> GetUnreadNotifications(Guid userId, Guid workspaceId)
    {
        var result = await repository
            .GetAllAsync(n => n.WorkspacesId.Contains(workspaceId) && n.UsersReceived != null && n.UsersReceived.Contains(userId));
        return result.Select(n => new NotificationDto { Id = n.Id, Data = n.Data });
    }

    public async Task MarkAsRead(Guid userId, IEnumerable<string> notificationsId)
    {
        var result = await repository.GetAllAsync(n => notificationsId.Contains(n.Id));
        List<string> notificationsToDelete = new();
        List<string> notificationsToReplace = new();

        var groups = result.GroupBy(n => n.UsersReceived == null || n.UsersReceived.Count() == 0);

        //Delete all notifications without user
        if (groups.Any(n => n.Key == true))
            await repository.DeleteManyByIdAsync(groups.First(n => n.Key == true).Select(x => x.Id));

        foreach (var notification in groups.First(n => n.Key == false).Select(x => x))
        {
            notification.UsersReceived.Remove(userId);
            await repository.UpdateAsync(notification);
        }
    }
}