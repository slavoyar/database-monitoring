using System.Text.Json;
using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;
using DatabaseMonitoring.BuildingBlocks.EventBusTest.CustomEvents;

public class EventOneHandler : IIntegrationEventHandler<CustomEventOne>
{
    public async Task Handle(CustomEventOne baseEvent)
    {
        System.Console.WriteLine($" I'm EventOneHandler, received: {JsonSerializer.Serialize(baseEvent)}");
    }
}