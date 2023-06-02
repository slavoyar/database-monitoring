using System;
using System.IO;
using System.Data.SQLite;

namespace PublishStatusWeb;

/// <summary>
/// Программа для перебора Web ресурсов, чтобы можно было проверить API
/// </summary>
class Program
{
    private static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri("http://localhost:5043/"),
    };
    
    private static async Task Main(string[] args)
    {
        //Список сайтов
        List<string> resultList = new List<string>();

        var path = "data.db";
        var fullPath = System.IO.Path.GetFullPath(path);
        var connectionString = $"Data Source={fullPath};Version=3;";

        await using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            const string sql = "SELECT address FROM web_list";
            await using (var command = new SQLiteCommand(sql, connection))
            {
                await using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = reader.GetString(0);
                        resultList.Add(result);
                    }
                }
            }
        }
        
        while (true)
        {
            foreach (var site in resultList)
            {
                //URL адрес API статуса сайта
                var url = $"/PingToWeb?nameOrAddress={site}";
                //Выполнение GET-запроса
                var response = await Client.GetAsync(url);
                //Получение ответа в виде строки
                var responseStatus = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Проверка: {site}");
                
                Thread.Sleep(5000);
            }
        }
    }
}