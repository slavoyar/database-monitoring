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

    /// <inheritdoc />
    public async Task PublishManyThroughEventBus(IEnumerable<BaseEvent> events)
    {
        if(events == null || events.Count() == 0)
            return;

        foreach (var @event in events)
        {
            await PublishThroughEventBus(@event);
        }
    }

    ///<inheritdoc />
    public Task PublishThroughEventBus(BaseEvent @event)
    {
        if(@event == null)
            return Task.CompletedTask;

        try
        {
            logger.LogInformation("Publishing application event:  {IntegrationEventId_published} - ({@IntegrationEvent})", @event.Id, @event);
            eventBus.Publish(@event);
        }
        catch(Exception e)
        {
            logger.LogError(e, "Error Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        }

        return Task.CompletedTask;
    }
}