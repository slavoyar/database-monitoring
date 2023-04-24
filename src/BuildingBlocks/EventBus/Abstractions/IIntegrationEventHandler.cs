namespace DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : BaseEvent
{
    Task Handle(TIntegrationEvent @event);
}

public interface IIntegrationEventHandler
{
}
