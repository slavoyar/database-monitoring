namespace DatabaseMonitoring.Services.Workspace.Infrustructure.Repository.Implementation;

public class EfRepository<T> : IRepository<T> where T : BaseEntity 
{
    private readonly AppDbContext context;
    private readonly DbSet<T> dbSet;
    public EfRepository(AppDbContext context)
    {
        dbSet = context.Set<T>();
        this.context = context;
    }

    /// <summary>
    /// Get workspace by id
    /// </summary>
    /// <returns><typeparamref name="T"/> or <typeparamref name="null"/></returns>
    public T Get(Guid id)
        => dbSet.Find(id);

    ///<summary>
    ///Get workspace by id asynchronous<br></br><br></br>
    ///<b>Returns</b> <typeparamref name="T"/> or <typeparamref name="null"/>
    ///</summmary>
    public async Task<T> GetAsync(Guid id)
        => await dbSet.FindAsync(id);

    ///<summary>
    ///Get all workspaces as queryable<br></br><br></br>
    ///<b>Returns</b> IQueryable<<typeparamref name="T"/>> or null
    ///</summmary
    public IQueryable<T> GetAll(bool asNoTracking = false)
        => asNoTracking ? dbSet.AsNoTracking() : dbSet;

    ///<summary>
    ///Get all workspaces <br></br><br></br>
    ///<b>Returns</b> ICollecion<<typeparamref name="T"/>> or null
    ///</summmary>
    public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        => await GetAll(asNoTracking).ToListAsync();


    ///<summary>
    ///Add new entity in repository <br></br><br></br>
    ///<b>Returns</b> Id of new entity
    ///</summmary>
    public Guid Create(T entity)
    {
        var result = dbSet.Add(entity);
        return result.Entity.Id;
    }

    ///<summary>
    ///Add new entity in repository asynchronous<br></br><br></br>
    ///<b>Returns</b> Id of new entity
    ///</summmary>
    public async Task<Guid> CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        return entity.Id;
    }

    ///<summary>
    ///Add range of entity in repository asynchronous<br></br><br></br>
    ///</summmary>
    public async Task CreateRangeAsync(ICollection<T> entities)
        => await dbSet.AddRangeAsync(entities);

    ///<summary>
    ///Remove entity from repository asynchronous<br></br><br></br>
    ///<b>Returns</b> True if entity was found, otherwise false
    ///</summmary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var obj = await dbSet.FindAsync(id);
        if(obj == null)
            return false;
        dbSet.Remove(obj);
        return true;

    }

    ///<summary>
    ///Remove entity from repository<br></br><br></br>
    ///<b>Returns</b> Flase if entity is null, otherwise true
    ///</summmary>
    public bool Delete(T entity)
    {
        if(entity == null)
            return false;
        context.Entry(entity).State = EntityState.Deleted;
        return true;
    }

    ///<summary>
    ///Remove range ofentity from repository<br></br><br></br>
    ///<b>Returns</b> False if collection is null or empty, otherwise true
    ///</summmary>
    public bool DeleteRange(ICollection<T> entities)
    {
        if(entities == null || !entities.Any())
            return false;
        dbSet.RemoveRange(entities);
        return true;
    }

    ///<summary>
    ///Update entity by entering its state <br></br><br></br>
    ///</summmary>
     public void Update(T entity)
        => context.Entry(entity).State = EntityState.Modified;

    ///<summary>
    ///Save changes asynchronous<br></br><br></br>
    ///<b>Returns</b> True if one or more entity was saved, othervise false
    ///</summmary>
    public async Task<bool> SaveChangesAsync()
    {
        if(await context.SaveChangesAsync() <= 0)
            return false;
        return true;
    }

}