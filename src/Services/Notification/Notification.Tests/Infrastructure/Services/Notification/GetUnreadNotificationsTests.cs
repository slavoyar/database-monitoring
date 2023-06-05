namespace Notifications.Tests.Infrastructure.Services.Notification;

public class GetUnreadNotificationsTests
{
    private readonly Mock<INotificationRepository> notificationRepositoryMock;
    private readonly NotificationService notificationService;
    public GetUnreadNotificationsTests()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        notificationRepositoryMock = fixture.Freeze<Mock<INotificationRepository>>();
        notificationService = fixture.Build<NotificationService>().OmitAutoProperties().Create();
    }

    [Fact]
    public async void GetUnreadNotifications_UsersReceivedIsEmpty_ResultIsEmpty()
    {
        var userId = Guid.NewGuid();
        var workspaceId = Guid.NewGuid();
        var notificationEntities = new List<NotificationEntity>();
        notificationRepositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<NotificationEntity,bool>>>()))
            .ReturnsAsync(notificationEntities);

        var restult = await notificationService.GetUnreadNotifications(userId, workspaceId);

        restult.Should().BeEmpty();
    }

    [Fact]
    public async void GetUnreadNotifications_UserHasUnreadNotifications_ReturnsNotifications()
    {
        
        var userId = Guid.NewGuid();
        var workspaceId = Guid.NewGuid();
        var notificationEntities = new List<NotificationEntity>
        {
            new NotificationEntity{Data="1", UsersReceived = new List<Guid>{userId}, WorkspacesId = new List<Guid>{workspaceId}},
            new NotificationEntity{Data="2", UsersReceived = new List<Guid>{userId}, WorkspacesId = new List<Guid>{workspaceId}}
        };

        notificationRepositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<NotificationEntity,bool>>>()))
            .ReturnsAsync(notificationEntities);

        var result = await notificationService.GetUnreadNotifications(userId, workspaceId);

        result.Should().BeAssignableTo<IEnumerable<NotificationDto>>();
        result.Should().HaveCount(2);
        result.Should().Contain(x => x.Data == "1");
        result.Should().Contain(x => x.Data == "2");
    }
}