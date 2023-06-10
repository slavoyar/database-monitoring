namespace DatabaseMonitoring.Services.Workspace.Core.Abstraction;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll(bool asNoTracking = false);
    Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);
    T Get(Guid id);
    Task<T> GetAsync(Guid id);

    Guid Create(T entity);
    Task<Guid> CreateAsync(T entity);
    Task CreateRangeAsync(ICollection<T> entities);

    Task<bool> DeleteAsync(Guid id);
    bool Delete(T entity);
    bool DeleteRange(ICollection<T> entities);

    void Update(T entity);
    Task<bool> SaveChangesAsync();

}