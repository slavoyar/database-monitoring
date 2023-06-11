namespace DatabaseMonitoring.Services.Workspace.Core.Abstraction;

/// <summary>
/// Base generic interface of repository
/// </summary>
/// <returns></returns>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Get all <typeparamref name="T"/> from database
    /// </summary>
    /// <param name="asNoTracking">Get as no tracking</param>
    /// <returns>IQueryable array of <typeparamref name="T"/></returns>
    IQueryable<T> GetAll(bool asNoTracking = false);

    /// <summary>
    /// Get all <typeparamref name="T"/> from database asynchronous
    /// </summary>
    /// <param name="cancellationToken">Token to cancell</param>
    /// <param name="asNoTracking">Get as no tracking</param>
    /// <returns>ICollection of <typeparamref name="T"/></returns>
    Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);

    /// <summary>
    /// Get <typeparamref name="T"/> by id
    /// </summary>
    /// <param name="id">Id of <typeparamref name="T"/></param>
    /// <returns><typeparamref name="T"/></returns>
    T Get(Guid id);

    /// <summary>
    /// Get <typeparamref name="T"/> by id asynchronous
    /// </summary>
    /// <param name="id">Id of <typeparamref name="T"/></param>
    /// <returns><typeparamref name="T"/></returns>
    Task<T> GetAsync(Guid id);

    /// <summary>
    /// Add new <typeparamref name="T"/> to database
    /// </summary>
    /// <param name="entity">New entity</param>
    /// <returns>New entity identifier</returns>
    Guid Create(T entity);

    /// <summary>
    /// Add new <typeparamref name="T"/> to database asynchronous
    /// </summary>
    /// <param name="entity">New entity</param>
    /// <returns>New entity identifier</returns>
    Task<Guid> CreateAsync(T entity);

    /// <summary>
    /// Add new range of <typeparamref name="T"/> to database asynchronous
    /// </summary>
    /// <param name="entities">New entities</param>
    Task CreateRangeAsync(ICollection<T> entities);


    /// <summary>
    /// Delete <typeparamref name="T"/> from database by id asynchronous
    /// </summary>
    /// <param name="id">Identifier of entity </param>
    /// <returns>True if entity was deleted succesfully, otherwise false</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Delete <typeparamref name="T"/> from database by id
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    /// <returns>True if entity was deleted succesfully, otherwise false</returns>
    bool Delete(T entity);

    /// <summary>
    /// Delete range of entities from database by id asynchronous
    /// </summary>
    /// <param name="entities">Identifier of entity </param>
    /// <returns>True if all entitities were deleted succesfully, otherwise false</returns>
    bool DeleteRange(ICollection<T> entities);

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity">Updated entity</param>
    void Update(T entity);

    /// <summary>
    /// Save changes to the database asynchronous
    /// </summary>
    /// <returns>True if one or more entity were affected, otherwise false</returns>
    Task<bool> SaveChangesAsync();

}