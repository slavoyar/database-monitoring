using System.Text.Json;
using DatabaseMonitoring.Ping_Connect.CustomEvents;
using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;

namespace DatabaseMonitoring.Ping_Connect.CustomEventsHandlers;

public class PingToWebEventsHandlers : IBaseEventHandler<PingToWebEvents>
{
    public async Task Handle(PingToWebEvents baseEvent)
    {
        Console.WriteLine($"PingToWebEventsHandlers, received: {JsonSerializer.Serialize(baseEvent)}");
    }
}