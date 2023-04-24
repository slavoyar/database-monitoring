using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;
using DatabaseMonitoring.BuildingBlocks.EventBusTest.CustomEvents;
using Microsoft.AspNetCore.Mvc;

namespace EventBusTest.Controllers;

[ApiController]
[Route("[controller]")]
public class SenderController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<SenderController> _logger;
    private readonly IEventBus eventBus;

    public SenderController(ILogger<SenderController> logger, IEventBus eventBus)
    {
        _logger = logger;
        this.eventBus = eventBus;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var isOne = new Random().Next(2) == 1;
        if (isOne)
        {
            var @event = new CustomEventOne("Something bad happend");
            eventBus.Publish(@event);
        }
        else
        {
            var @event = new CustomEventTwo("Something good happend");
            eventBus.Publish(@event);
        }

        return Ok(isOne ? "1" : "2");
    }
}
