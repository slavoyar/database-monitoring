using DatabaseMonitoring.BuildingBlocks.EventBus.Events;

namespace DatabaseMonitoring.Ping_Connect.CustomEvents;

public record ConnectToDatabaseEvents : BaseEvent
{
    public ConnectToDatabaseEvents()
    {

    }
    public ConnectToDatabaseEvents(string info)
    {
        this.info = info;
    }
    public string info { get; set; } = "ConnectToDatabaseEvents";
}