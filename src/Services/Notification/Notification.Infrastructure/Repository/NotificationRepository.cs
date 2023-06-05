using DatabaseMonitoring.Services.Notification.Infrastructure.Data;

namespace DatabaseMonitoring.Services.Notification.Infrastructure.Repository;

public class NotificationRepository : MongoDbRepository<NotificationEntity>, INotificationRepository
{
    private readonly IMongoCollection<NotificationEntity> collection;
    public NotificationRepository(MongoDb mongoDb) : base(mongoDb)
    {
        collection = mongoDb.Database.GetCollection<NotificationEntity>(GetCollectionName(typeof(NotificationEntity)));
    }

    public async Task<NotificationEntity> UpdateAsync(NotificationEntity entity)
    {
        var entityFromDb = await collection.FindOneAndReplaceAsync(e => e.Id == entity.Id, entity);
        return entityFromDb;
    }

}