using System.Text.Json;
using DatabaseMonitoring.Ping_Connect.CustomEvents;
using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;

namespace DatabaseMonitoring.Ping_Connect.CustomEventsHandlers;

public class ConnectToDatabaseEventsHandlers : IBaseEventHandler<ConnectToDatabaseEvents>
{
    public async Task Handle(ConnectToDatabaseEvents baseEvent)
    {
        Console.WriteLine($"ConnectToDatabaseEventsHandlers, received: {JsonSerializer.Serialize(baseEvent)}");
    }
}