namespace EventBus.Tests;

public class InMemory_SubscriptionManager_Tests
{
    [Fact]
    public void InMemorySubscriptionManager_OnCreation_IsEmpty()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        Assert.True(manager.IsEmpty);
    }

    [Fact]
    public void InMemorySubscription_OnAddSubscription_ContainsEvent()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.AddSubscription<TestBaseEvent, TestBaseEventHandler>();
        Assert.True(manager.HasSubscriptionsForEvent<TestBaseEvent>());
    }

    [Fact]
    public void InMemorySubscriptionManager_OnDeleteAllSubscriptions_EventDoesNotExist()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.AddSubscription<TestBaseEvent, TestBaseEventHandler>();
        manager.RemoveSubscription<TestBaseEvent, TestBaseEventHandler>();
        Assert.False(manager.HasSubscriptionsForEvent<TestBaseEvent>());
    }

    [Fact]
    public void InMemorySubscriptionManager_OnLastSubscriptionDeleting_RaiseEvent()
    {
        bool raised = false;
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.OnEventRemoved += (o, e) => raised = true;
        manager.AddSubscription<TestBaseEvent, TestBaseEventHandler>();
        manager.RemoveSubscription<TestBaseEvent, TestBaseEventHandler>();
        Assert.True(raised);
    }

    [Fact]
    public void InMemorySubscriptionManager_OnGetHandlers_ReturnsAllHandlers()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.AddSubscription<TestBaseEvent, TestBaseEventHandler>();
        manager.AddSubscription<TestBaseEvent, TestBaseOtherEventHandler>();
        var handlers = manager.GetHandlersForEvent<TestBaseEvent>();
        Assert.Equal(2, handlers.Count());
    }
}