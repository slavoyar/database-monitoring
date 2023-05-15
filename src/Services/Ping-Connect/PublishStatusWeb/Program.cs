using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

/// <summary>
/// Программа для перебора Web ресурсов, чтобы можно было проверить API
/// </summary>
class Program
{
    private static HttpClient client = new()
    {
        BaseAddress = new Uri("http://localhost:5043/"),
    };
    
    private static async Task Main(string[] args)
    {
        //Список сайтов
        //TODO:брать список сайтов из БД
        var mySites = new string[] { "yandex.ru", "youtube.com", "miro.com","github.com","metanit.com","m1etanit1.com" };
        
        while (true)
        {
            //var count = 1;
            foreach (var site in mySites)
            {
                //URL адрес API статуса сайта
                var url = $"/PingToWeb?nameOrAddress={site}";
                
                //Выполнение GET-запроса
                var response = await client.GetAsync(url);
                
                //Получение ответа в виде строки
                var responseStatus = await response.Content.ReadAsStringAsync();
                
                //Отправляем в очередь статус сайта
                //PublishStatusWebRabbitMQ(responseStatus, count);
                //count++;
                Thread.Sleep(5000);
            }
        }
    }
    
    // static void PublishStatusWebRabbitMQ(string status, int count)
    // {
    //     //Создаем фабрику подключений
    //     var factory = new ConnectionFactory() { HostName = "localhost" };
    //     //Создаем подключение
    //     using var connection = factory.CreateConnection();
    //     //Создаем канал
    //     using var channel = connection.CreateModel();
    //     //Создаем очередь
    //     channel.QueueDeclare(queue: "StatusWebSite",
    //         durable: false,
    //         exclusive: false,
    //         autoDelete: false,
    //         arguments: null);
    //
    //     // Создаем exchanger с именем "Status" и типом "direct"
    //     channel.ExchangeDeclare(exchange: "Status", type: ExchangeType.Direct);
    //     
    //     //Создаем сообщение
    //     var message = status;
    //     var body = Encoding.UTF8.GetBytes(message);
    //
    //     //Отправляем сообщение в очередь
    //     channel.BasicPublish(exchange: "Status",
    //         routingKey: "StatusWebSite",
    //         basicProperties: null,
    //         body: body);
    //     Console.WriteLine($" [{count}] Sent : {message}");
    // }
}
