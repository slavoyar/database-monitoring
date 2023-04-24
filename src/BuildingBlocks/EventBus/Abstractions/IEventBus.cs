namespace DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;

public interface IEventBus
{
    void Publish(BaseEvent @event);

    void Subscribe<T, TH>()
        where T : BaseEvent
        where TH : IIntegrationEventHandler<T>;

    void SubscribeDynamic<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    void UnsubscribeDynamic<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    void Unsubscribe<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : BaseEvent;
}
