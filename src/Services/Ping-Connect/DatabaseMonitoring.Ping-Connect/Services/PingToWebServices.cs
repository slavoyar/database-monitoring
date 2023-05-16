using Newtonsoft.Json;
using System.Net.NetworkInformation;
using DatabaseMonitoring.Ping_Connect.Models;

namespace DatabaseMonitoring.Ping_Connect.Services;

public static class PingToWebServices
{
    /// <summary>
    /// Логика проверки Web ресурса
    /// </summary>
    /// <param name="nameOrAddress">Адрес ресурса</param>
    /// <returns></returns>
    public static async Task<string> GetPingHostAsync(string nameOrAddress)
    {
        //Очищаем адрес для проверки.
        nameOrAddress = nameOrAddress.Replace("https://", "")
            .Replace("http://", "")
            .Replace("/", "");
        
        var time = DateTime.Now;
        var infoPingStatus = new ServerPingStatusPublished();
        
        try
        {
            using var ping = new Ping();
            var reply = ping.Send(nameOrAddress);
            Console.WriteLine($"{time} : [INFO] : Success ping - \"{nameOrAddress}\"");
            infoPingStatus.Time = time;
            infoPingStatus.Info = reply.Status.ToString();
            infoPingStatus.PingStatus = reply.Status.ToString();
            infoPingStatus.ServerId = nameOrAddress;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{time} : [ERROR] : Error ping - \"{nameOrAddress}\": " + ex.Message);
            infoPingStatus.Time = time;
            infoPingStatus.Info = ex.Message;
            infoPingStatus.PingStatus = "Error";
            infoPingStatus.ServerId = nameOrAddress;
        }

        var infoPing = JsonConvert.SerializeObject(infoPingStatus);
        
        return infoPing;
    }
}