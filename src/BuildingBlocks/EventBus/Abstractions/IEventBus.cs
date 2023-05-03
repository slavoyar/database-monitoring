namespace DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;

public interface IEventBus
{
    void Publish(BaseEvent @event);

    void Subscribe<T, TH>()
        where T : BaseEvent
        where TH : IBaseEventHandler<T>;

    void SubscribeDynamic<TH>(string eventName)
        where TH : IDynamicBaseEventHandler;

    void UnsubscribeDynamic<TH>(string eventName)
        where TH : IDynamicBaseEventHandler;

    void Unsubscribe<T, TH>()
        where TH : IBaseEventHandler<T>
        where T : BaseEvent;
}
