namespace DatabaseMonitoring.Services.Notification.Infrastructure.Repository;

public class NotificationRepository : MongoDbRepository<NotificationEntity>, INotificationRepository
{
    private readonly IMongoCollection<NotificationEntity> collection;
    public NotificationRepository(IOptions<MongoDbConfiguration> options) : base(options)
    {
        collection = new MongoClient(options.Value.ConnectionString)
            .GetDatabase(options.Value.DataBaseName).GetCollection<NotificationEntity>(GetCollectionName(typeof(NotificationEntity)));
    }

    public async Task<NotificationEntity> UpdateAsync(NotificationEntity entity)
    {
        var entityFromDb = await collection.FindOneAndReplaceAsync(e => e.Id == entity.Id, entity);
        return entityFromDb;
    }

}