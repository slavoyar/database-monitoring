namespace DatabaseMonitoring.Services.Workspace.ApplicationEvents;

/// <summary>
/// Interface for working with message bus
/// </summary>
public interface IApplicationEventService
{
    /// <summary>
    /// Publish new event to event bus
    /// </summary>
    /// <param name="event">Event to publish</param>
    Task PublishThroughEventBus(BaseEvent @event);

    /// <summary>
    /// Publish new events to event bus
    /// </summary>
    /// <param name="events">Events to publish</param>
    Task PublishManyThroughEventBus(IEnumerable<BaseEvent> events);
}