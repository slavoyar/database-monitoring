namespace Notifications.Tests.Infrastructure.Services.Notification;

public class MarkAsReadTests
{
    private readonly Mock<INotificationRepository> notificationRepositoryMock;
    private readonly NotificationService notificationService;
    public MarkAsReadTests()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        notificationRepositoryMock = fixture.Freeze<Mock<INotificationRepository>>();
        notificationService = fixture.Build<NotificationService>().OmitAutoProperties().Create();
    }

    [Fact]
    public async void MarkAsReadTests_UsersReceivedNotEmpty_RemovedCurrentUserId()
    {
        var userId = Guid.NewGuid();
        var notifications = GetTestListOfNotificationEntities(userId);
        var notificationsId = notifications.Select(x => x.Id);

        notificationRepositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<NotificationEntity,bool>>>()))
            .ReturnsAsync(notifications);

        await notificationService.MarkAsRead(userId, notificationsId);

        notifications.Where(n => n.Receivers.Contains(userId)).Should().BeEmpty();
    }

    [Fact]
    public async void MarkAsReadTests_UsersReceivedIsEmpty_DeleteNotificationsFromDb()
    {
        var notifications = GetTestListOfNotificationEntities(Guid.NewGuid());
        var userId = notifications.Last().Receivers.First();
        var notificationsId = notifications.Select(x => x.Id);

        notificationRepositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<NotificationEntity,bool>>>()))
            .ReturnsAsync(notifications);

        await notificationService.MarkAsRead(userId, notificationsId);

        notificationRepositoryMock
            .Verify(x => x.DeleteManyByIdAsync(It.Is<IEnumerable<string>>(n => n.Any(id => id == notifications.Last().Id))), Times.Once);
    }

    private IEnumerable<NotificationEntity> GetTestListOfNotificationEntities(Guid userId)
    {
        return new List<NotificationEntity>
        {
            new NotificationEntity
            {
                Id = Guid.NewGuid().ToString(),
                Receivers = new List<Guid>
                {
                    Guid.NewGuid(),
                    userId
                },
                WorkspacesId = new List<Guid>
                {
                    Guid.NewGuid(),
                }
            },
            new NotificationEntity
            {
                Id = Guid.NewGuid().ToString(),
                Receivers = new List<Guid>
                {
                    Guid.NewGuid(),
                    userId
                },
                WorkspacesId = new List<Guid>
                {
                    Guid.NewGuid(),
                }
            },
            new NotificationEntity
            {
                Id = Guid.NewGuid().ToString(),
                Receivers = new List<Guid>
                {
                    Guid.NewGuid(),
                },
                WorkspacesId = new List<Guid>
                {
                    Guid.NewGuid(),
                }
            },
        };
    }
}