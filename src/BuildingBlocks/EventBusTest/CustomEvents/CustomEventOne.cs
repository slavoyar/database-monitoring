namespace DatabaseMonitoring.BuildingBlocks.EventBusTest.CustomEvents;

using DatabaseMonitoring.BuildingBlocks.EventBus.Events;

public record CustomEventOne : BaseEvent
{
    public CustomEventOne()
    {

    }
    public CustomEventOne(string info)
    {
        this.info = info;
    }
    public string info { get; set; } = "I'm custom event one";
}