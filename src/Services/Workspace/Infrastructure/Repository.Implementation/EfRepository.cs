namespace DatabaseMonitoring.Services.Workspace.Infrustructure.Repository.Implementation;

/// <summary>
/// Entity framework implementation of IRepository
/// </summary>
public class EfRepository<T> : IRepository<T> where T : BaseEntity 
{
    private readonly AppDbContext context;
    private readonly DbSet<T> dbSet;

    /// <summary>
    /// Entity framework implementation of IRepository contructor
    /// </summary>
    public EfRepository(AppDbContext context)
    {
        dbSet = context.Set<T>();
        this.context = context;
    }

    /// <inheritdoc/>
    public T Get(Guid id)
        => dbSet.Find(id);

    /// <summary>
    /// Get entity by id asynchronous
    /// </summary>
    /// <param name="id">Identifier of entity</param>
    /// <returns><typeparamref name="T"/> or null</returns>
    public async Task<T> GetAsync(Guid id)
        => await dbSet.FindAsync(id);

    /// <summary>
    /// Get all entity of type <typeparamref name="T"/>
    /// </summary>
    /// <param name="asNoTracking">Get as no tracking</param>
    /// <returns>IQueryable of <typeparamref name="T"/></returns>
    public IQueryable<T> GetAll(bool asNoTracking = false)
        => asNoTracking ? dbSet.AsNoTracking() : dbSet;

    /// <summary>
    /// Get all entity of type <typeparamref name="T"/> asynchronous
    /// </summary>
    /// <param name="asNoTracking">Get as no tracking</param>
    /// <param name="cancellationToken">Token to cancell</param>
    /// <returns>IQueryable of <typeparamref name="T"/></returns>
    public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        => await GetAll(asNoTracking).ToListAsync();


    /// <summary>
    /// Add new entity to database
    /// </summary>
    /// <param name="entity">New entity</param>
    /// <returns>Generated identifier</returns>
    public Guid Create(T entity)
    {
        var result = dbSet.Add(entity);
        return result.Entity.Id;
    }

    /// <summary>
    /// Add new entity to database asynchronous
    /// </summary>
    /// <param name="entity">New entity</param>
    /// <returns>Generated identifier</returns>
    public async Task<Guid> CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        return entity.Id;
    }

    /// <summary>
    /// Add range of entities to database asynchronous
    /// </summary>
    /// <param name="entities">New collection of entity</param>
    public async Task CreateRangeAsync(ICollection<T> entities)
        => await dbSet.AddRangeAsync(entities);

    /// <summary>
    /// Delete <typeparamref name="T"/> by id from database asynchronous
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <returns>True if entity was deleted succesfully, otherwise false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var obj = await dbSet.FindAsync(id);
        if(obj == null)
            return false;
        dbSet.Remove(obj);
        return true;

    }

    /// <summary>
    /// Delete <typeparamref name="T"/> by id from database
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    /// <returns>True if entity was deleted succesfully, otherwise false</returns>
    public bool Delete(T entity)
    {
        if(entity == null)
            return false;
        context.Entry(entity).State = EntityState.Deleted;
        return true;
    }

    /// <summary>
    /// Delete range of <typeparamref name="T"/>from database asynchronous
    /// </summary>
    /// <param name="entities">Entities to delte</param>
    /// <returns>True if entities were deleted succesfully, otherwise false</returns>
    public bool DeleteRange(ICollection<T> entities)
    {
        if(entities == null || !entities.Any())
            return false;
        dbSet.RemoveRange(entities);
        return true;
    }

    /// <summary>
    /// Update <typeparamref name="T"/>
    /// </summary>
    /// <param name="entity">Updated entity</param>
     public void Update(T entity)
        => context.Entry(entity).State = EntityState.Modified;

    /// <summary>
    /// Saves changes asynchronous
    /// </summary>
    /// <returns>True if one or more entities were affected, otherwise false</returns>
    public async Task<bool> SaveChangesAsync()
    {
        if(await context.SaveChangesAsync() <= 0)
            return false;
        return true;
    }

}