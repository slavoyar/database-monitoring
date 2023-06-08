namespace EventBus.Tests.Events;

public class TestNewNotificationCreatedEvent
{
    [Fact]
    public void NewNotificationCreatedEvent_WithWorkspaceId_ServerIdIsNull()
    {
        var notification = NewNotificationCreatedEvent.WithWorkspaceId(Guid.NewGuid(), string.Empty);

        Assert.True(notification.ServerId == null);
    }
    [Fact]
    public void NewNotificationCreatedEvent_WithServerId_WorkspaceIdIsNull()
    {
        var notification = NewNotificationCreatedEvent.WithServerId(Guid.NewGuid(), string.Empty);

        Assert.True(notification.WorkspaceId == null);
    }
}