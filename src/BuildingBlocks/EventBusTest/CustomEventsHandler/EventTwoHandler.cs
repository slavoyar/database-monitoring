using System.Text.Json;
using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;
using DatabaseMonitoring.BuildingBlocks.EventBusTest.CustomEvents;

public class EventTwoHandler : IIntegrationEventHandler<CustomEventTwo>
{
    public async Task Handle(CustomEventTwo baseEvent)
    {
        System.Console.WriteLine($" I'm EventTwoHandler, received: {JsonSerializer.Serialize(baseEvent)}");
    }
}