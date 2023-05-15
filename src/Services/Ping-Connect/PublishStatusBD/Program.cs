using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

/// <summary>
/// Программа для перебора БД, чтобы можно было проверить API
/// </summary>
class Program
{
    private static HttpClient client = new()
    {
        BaseAddress = new Uri("http://localhost:5043/"),
    };
    
    private static async Task Main(string[] args)
    {
        //Список БД
        //TODO:брать список БД из БД
        var myBDs = new string[] { "bd=mySQL&host=192.168.0.1&username=test&password=test&database=testBD",
                                   "bd=postgresql&host=localhost&username=postgres&password=8888&database=postgres" };

        while (true)
        {
            //int count = 1;
            
            foreach (var bd in myBDs)
            {
                //URL адрес API статуса БД
                var url = $"/ConnectToDatabase?{bd}";

                //Выполнение GET-запроса
                var response = await client.GetAsync(url);
                
                //Получение ответа в виде строки
                var responseStatus = await response.Content.ReadAsStringAsync();
                
                //Отправляем в очередь статус БД
                //PublishStatusBDRabbitMQ(responseStatus, count);
                //count++;
                Thread.Sleep(25000);
            }
        }
    }
    
    // static void PublishStatusBDRabbitMQ(string status, int count)
    // {
    //     //Создаем фабрику подключений
    //     var factory = new ConnectionFactory() { HostName = "localhost" };
    //     //Создаем подключение
    //     using var connection = factory.CreateConnection();
    //     //Создаем канал
    //     using var channel = connection.CreateModel();
    //     //Создаем очередь
    //     channel.QueueDeclare(queue: "StatusBD",
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
    //         routingKey: "StatusBD",
    //         basicProperties: null,
    //         body: body);
    //     Console.WriteLine($" [{count}] Sent : {message}");
    // }
}
