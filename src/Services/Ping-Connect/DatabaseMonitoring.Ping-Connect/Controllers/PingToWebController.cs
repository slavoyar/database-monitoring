using DatabaseMonitoring.Ping_Connect.CustomEvents;
using Microsoft.AspNetCore.Mvc;
using DatabaseMonitoring.Ping_Connect.Services;
using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;

namespace DatabaseMonitoring.Ping_Connect.Controllers;

/// <summary>
/// Контроллер проверки доступа к Web ресурсу
/// </summary>
[ApiController]
[Route("[controller]")]
public class PingToWebController : ControllerBase
{
    private readonly ILogger<PingToWebController> _logger;
    private readonly IEventBus eventBus;

    public PingToWebController(ILogger<PingToWebController> logger, IEventBus eventBus)
    {
        _logger = logger;
        this.eventBus = eventBus;
    }

    /// <summary>
    /// Проверка доступа к Web ресурсу
    /// </summary>
    /// <param name="nameOrAddress">Адрес ресурса</param>
    /// <returns>JSON</returns>
    [HttpGet(Name = "GetPingHost")]
    public async Task<string> GetPingHost(string nameOrAddress)
    {
        var result = await PingToWebServices.GetPingHostAsync(nameOrAddress);
        var @event = new PingToWebEvents(result);
        eventBus.Publish(@event);

        return result;
    }
}