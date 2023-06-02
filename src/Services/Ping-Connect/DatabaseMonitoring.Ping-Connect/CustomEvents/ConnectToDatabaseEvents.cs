using DatabaseMonitoring.BuildingBlocks.EventBus.Events;
using DatabaseMonitoring.Ping_Connect.Models;

namespace DatabaseMonitoring.Ping_Connect.CustomEvents;

public record ConnectToDatabaseEvents : BaseEvent
{
    public ConnectToDatabaseEvents()
    {

    }
    public ConnectToDatabaseEvents(ServerPingStatusPublished info)
    {
        this.Id = info.Id;
        this.Status = info.Status;
        this.Error = info.Error;
    }
    public string? Id { get; set; } = null;
    public string? Status { get; set; } = null;   
    public string? Error { get; set; } = null;
}