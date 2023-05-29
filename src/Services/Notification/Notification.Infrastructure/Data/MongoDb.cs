namespace DatabaseMonitoring.Services.Notification.Infrastructure.Data;

public class MongoDb
{
    private readonly MongoDbConfiguration cfg;
    public IMongoDatabase Database { get; private set; }

    public MongoDb(IOptions<MongoDbConfiguration> options)
    {
        cfg = options.Value;
        var i = new MongoClient(cfg.ConnectionString).GetDatabase(cfg.DataBaseName);
    }
}