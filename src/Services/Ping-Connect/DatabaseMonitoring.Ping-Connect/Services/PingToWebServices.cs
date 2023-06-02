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
    public static async Task<ServerPingStatusPublished> GetPingHostAsync(string nameOrAddress)
    {
        //Очищаем адрес для проверки.
        nameOrAddress = ReplaceNameOrAddress(nameOrAddress);
        
        var infoPingStatus = new ServerPingStatusPublished();
        using var ping = new Ping();
        
        try
        {
            var reply = ping.Send(nameOrAddress);
            //Console.WriteLine($"{time} : [INFO] : Success ping - \"{nameOrAddress}\"");
            infoPingStatus.Id = nameOrAddress;
            infoPingStatus.Status = "Success";
            infoPingStatus.Error = "";
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"{time} : [ERROR] : Error ping - \"{nameOrAddress}\": " + ex.Message);
            infoPingStatus.Id = nameOrAddress;
            infoPingStatus.Status = "Error";
            infoPingStatus.Error = ex.Message;
        }
        
        return await Task.FromResult(infoPingStatus);
    }

    private static string ReplaceNameOrAddress(string nameOrAddress)
    {
        return nameOrAddress.Replace("https://", "")
            .Replace("http://", "")
            .Replace("/", "");
    }
}