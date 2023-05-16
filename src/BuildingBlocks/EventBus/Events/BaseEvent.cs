namespace DatabaseMonitoring.BuildingBlocks.EventBus.Events;

public record BaseEvent
{
    public BaseEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    [JsonConstructor]
    public BaseEvent(Guid id, DateTime createDate)
    {
        Id = id;
        CreationDate = createDate;
    }

    [JsonInclude]
    public Guid Id { get; private init; }

    [JsonInclude]
    public DateTime CreationDate { get; private init; }
}
