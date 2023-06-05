namespace DatabaseMonitoring.Services.Notification.Infrastructure.Data;

public class MongoDb
{
    public IMongoDatabase Database { get; }

    public MongoDb(IOptions<MongoDbConfiguration> options)
    {
        var cfg = options.Value;
        Database = new MongoClient(cfg.ConnectionString).GetDatabase(cfg.DataBaseName);
    }
}