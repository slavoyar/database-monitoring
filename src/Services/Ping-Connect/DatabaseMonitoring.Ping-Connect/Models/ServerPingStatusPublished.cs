using System.Net.NetworkInformation;

namespace DatabaseMonitoring.Ping_Connect.Models;

/// <summary>
/// Модель получения информации
/// </summary>
public class ServerPingStatusPublished
{
    public DateTime Time { get; set; }
    public string Info { get; set; }
    public string PingStatus { get; set; }
    public string ServerId { get; set; }
}