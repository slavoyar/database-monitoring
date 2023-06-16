namespace DatabaseMonitoring.Services.Workspace.ApplicationEvents;

/// <summary>
/// Application event service impl
/// </summary>
public class ApplicationEventService : IApplicationEventService
{
    private readonly ILogger<ApplicationEventService> logger;
    private readonly IEventBus eventBus;

    /// <summary>
    /// Contsructor
    /// </summary>
    public ApplicationEventService(
        ILogger<ApplicationEventService> logger,
        IEventBus eventBus    
        )
    {
        this.logger = logger;
        this.eventBus = eventBus;
    }
    ///<inheritdoc />
    public void PublishThroughEventBusAsync(BaseEvent @event)
    {
        try
        {
            logger.LogInformation("Publishing application event:  {IntegrationEventId_published} - ({@IntegrationEvent})", @event.Id, @event);
            eventBus.Publish(@event);
        }
        catch(Exception e)
        {
            logger.LogError(e, "Error Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        }
    }
}