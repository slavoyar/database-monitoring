namespace DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;

public interface IBaseEventHandler<in TBaseEvent> : IBaseEventHandler
    where TBaseEvent : BaseEvent
{
    Task Handle(TBaseEvent @event);
}

public interface IBaseEventHandler
{
}
