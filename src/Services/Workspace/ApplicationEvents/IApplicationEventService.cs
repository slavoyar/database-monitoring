namespace DatabaseMonitoring.Services.Workspace.ApplicationEvents;

/// <summary>
/// Interface for working with message bus
/// </summary>
public interface IApplicationEventService
{
    /// <summary>
    /// Description
    /// </summary>
    /// <param name="event">Event to publish</param>
    void PublishThroughEventBus(BaseEvent @event);
}