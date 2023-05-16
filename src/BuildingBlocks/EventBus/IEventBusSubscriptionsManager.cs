namespace DatabaseMonitoring.BuildingBlocks.EventBus;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;
    void AddDynamicSubscription<TH>(string eventName)
        where TH : IDynamicBaseEventHandler;

    void AddSubscription<T, TH>()
        where T : BaseEvent
        where TH : IBaseEventHandler<T>;

    void RemoveSubscription<T, TH>()
            where TH : IBaseEventHandler<T>
            where T : BaseEvent;
    void RemoveDynamicSubscription<TH>(string eventName)
        where TH : IDynamicBaseEventHandler;

    bool HasSubscriptionsForEvent<T>() where T : BaseEvent;
    bool HasSubscriptionsForEvent(string eventName);
    Type GetEventTypeByName(string eventName);
    void Clear();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : BaseEvent;
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
    string GetEventKey<T>();
}
