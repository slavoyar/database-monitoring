namespace DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;

public interface IDynamicBaseEventHandler
{
    Task Handle(dynamic eventData);
}
