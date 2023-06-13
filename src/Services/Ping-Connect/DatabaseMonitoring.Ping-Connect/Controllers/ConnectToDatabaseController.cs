using DatabaseMonitoring.Ping_Connect.CustomEvents;
using Microsoft.AspNetCore.Mvc;
using DatabaseMonitoring.Ping_Connect.Services;
using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;
using DatabaseMonitoring.Ping_Connect.Models;

namespace DatabaseMonitoring.Ping_Connect.Controllers;

/// <summary>
/// Контроллер проверки доступа к БД
/// </summary>
[ApiController]
[Route("[controller]")]
public class ConnectToDatabaseController : ControllerBase
{
    private readonly ILogger<ConnectToDatabaseController> _logger;
    private readonly IEventBus eventBus;

    public ConnectToDatabaseController(ILogger<ConnectToDatabaseController> logger, IEventBus eventBus)
    {
        _logger = logger;
        this.eventBus = eventBus;
    }

    /// <summary>
    /// Проверка доступа к БД
    /// </summary>
    /// <param name="bd">Тип БД</param>
    /// <param name="host">Хост БД</param>
    /// <param name="username">Логин</param>
    /// <param name="password">Пароль</param>
    /// <param name="database">База данных</param>
    [HttpGet(Name = "GetConnectToDatabase")]
    public async Task<ServerPingStatusPublished> GetConnectToDatabase(string bd, string host, string username, string password, string database)
    {
        var result = await ConnectToDatabaseServices.GetConnectToDatabaseAsync(bd, host, username, password, database);
        if (result.Status != "Error") return result;
        var @event = new ConnectToDatabaseEvents(result);
        eventBus.Publish(@event);
        
        return result;
    }
}