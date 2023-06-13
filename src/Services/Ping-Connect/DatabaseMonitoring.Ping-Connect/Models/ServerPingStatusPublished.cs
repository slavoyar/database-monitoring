using System.Net.NetworkInformation;

namespace DatabaseMonitoring.Ping_Connect.Models;

/// <summary>
/// Модель получения информации
/// </summary>
public class ServerPingStatusPublished
{
    public string Id { get; set; }
    public string Status { get; set; }
    public string Error { get; set; }
}