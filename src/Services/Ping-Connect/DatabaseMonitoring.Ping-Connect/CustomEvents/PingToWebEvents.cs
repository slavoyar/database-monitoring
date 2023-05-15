using DatabaseMonitoring.BuildingBlocks.EventBus.Events;

namespace DatabaseMonitoring.Ping_Connect.CustomEvents;

public record PingToWebEvents : BaseEvent
{
    public PingToWebEvents()
    {

    }
    public PingToWebEvents(string info)
    {
        this.info = info;
    }
    public string info { get; set; } = "PingToWebEvents";
}