namespace Notifications.Tests.Infrastructure.Models;

public class NewNotificationDtoTests
{
    [Fact]
    public void NewNotificationDto_CorrectMapsTo_NotificationEntity()
    {
        // Given
        var newNotification = new Fixture().Create<NewNotificationDto>();
        // When
        var notificationEntity = NewNotificationDto.MapToNotificationEntity(newNotification);
        // Then
        notificationEntity.CreationDate.Should().BeSameDateAs(newNotification.CreationDate);
        notificationEntity.Data.Should().BeSameAs(newNotification.Data);
        notificationEntity.UsersReceived.Should().BeSameAs(newNotification.UsersId);
        notificationEntity.WorkspacesId.Should().BeSameAs(newNotification.WorkspacesId);
    }
}

