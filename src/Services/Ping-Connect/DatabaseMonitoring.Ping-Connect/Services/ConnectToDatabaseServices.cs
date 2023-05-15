using Npgsql;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using DatabaseMonitoring.Ping_Connect.Models;

namespace DatabaseMonitoring.Ping_Connect.Services;

public static class ConnectToDatabaseServices
{
    /// <summary>
    /// Логика проверки БД
    /// </summary>
    /// <param name="bd">Тип БД</param>
    /// <param name="host">Хост БД</param>
    /// <param name="username">Логин</param>
    /// <param name="password">Пароль</param>
    /// <param name="database">База данных</param>
    /// <returns></returns>
    public static async Task<string> GetConnectToDatabaseAsync(string bd, string host, string username, string password, string database)
    {
        var time = DateTime.Now;
        var infoPingStatus = new ServerPingStatusPublished();
        
        try
        {
            if (bd == "postgresql")
            {
                //Host=localhost;Username=postgres;Password=8888;Database=postgres
                await using var connection = new NpgsqlConnection($"Host={host};Username={username};Password={password};Database={database};");
                connection.Open();
                Console.WriteLine($"{time} : [INFO] : Success ping - \"{host}\" - \"{database}\"");
                infoPingStatus.Time = time;
                infoPingStatus.Info = connection.ToString();
                infoPingStatus.PingStatus = "Success";
                infoPingStatus.ServerId = $"\"{host}\" - \"{database}\"";
            }
            else
            {
                //Server=localhost;Database=mysql;Uid=test;Pwd=test;
                await using var connection = new MySqlConnection($"Server={host};Uid={username};Pwd={password};Database={database};");
                connection.Open();
                Console.WriteLine($"{time} : [INFO] : Success ping - \"{host}\" - \"{database}\"");
                infoPingStatus.Time = time;
                infoPingStatus.Info = connection.ToString();
                infoPingStatus.PingStatus = "Success";
                infoPingStatus.ServerId = $"\"{host}\" - \"{database}\"";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{time} : [ERROR] : Error ping - \"{host}\" - \"{database}\" " + ex.Message);
            infoPingStatus.Time = time;
            infoPingStatus.Info = ex.Message;
            infoPingStatus.PingStatus = "Error";
            infoPingStatus.ServerId = "\"{host}\" - \"{database}\"";
        }
        
        var infoPing = JsonConvert.SerializeObject(infoPingStatus);
        
        return infoPing;
    }
}