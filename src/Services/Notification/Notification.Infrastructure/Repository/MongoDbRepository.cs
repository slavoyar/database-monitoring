namespace DatabaseMonitoring.Services.Notification.Infrastructure.Repository;

public class MongoDbRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> collection;
    public MongoDbRepository(MongoDb mongoDb)
    {
        collection = mongoDb.Database.GetCollection<T>(GetCollectionName(typeof(T)));
    }

    private string GetCollectionName(Type collectionType)
        => ((BsonCollectionAttribute)collectionType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault())?.CollectionName;

    public async Task<string> CreateAsync(T entity)
    {
        await collection.InsertOneAsync(entity);
        return entity.Id;
    }

    public async Task DeleteManyByIdAsync(IEnumerable<string> ids)
    {
        await collection.DeleteManyAsync(e => ids.Contains(e.Id));
    }

    public async Task DeleteOneByIdAsync(string id)
    {
        await collection.DeleteOneAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> exspression = null)
    {
        var result = await collection.FindAsync(exspression);
        return await result.ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
        => await GetFristOrDefaultAsync(e => e.Id == id);

    public async Task<T> GetFristOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        var result = await collection.FindAsync(expression);
        return result.First();
    }
}