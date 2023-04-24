namespace DatabaseMonitoring.BuildingBlocks.EventBus;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;
    void AddDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    void AddSubscription<T, TH>()
        where T : BaseEvent
        where TH : IIntegrationEventHandler<T>;

    void RemoveSubscription<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : BaseEvent;
    void RemoveDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    bool HasSubscriptionsForEvent<T>() where T : BaseEvent;
    bool HasSubscriptionsForEvent(string eventName);
    Type GetEventTypeByName(string eventName);
    void Clear();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : BaseEvent;
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
    string GetEventKey<T>();
}
