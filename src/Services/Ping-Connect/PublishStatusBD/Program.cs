namespace PublishStatusBD;

/// <summary>
/// Программа для перебора БД, чтобы можно было проверить API
/// </summary>
class Program
{
    private static readonly HttpClient Client = new()
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
            foreach (var bd in myBDs)
            {
                //URL адрес API статуса БД
                var url = $"/ConnectToDatabase?{bd}";
                //Выполнение GET-запроса
                var response = await Client.GetAsync(url);
                //Получение ответа в виде строки
                var responseStatus = await response.Content.ReadAsStringAsync();
                
                Thread.Sleep(25000);
            }
        }
    }
}