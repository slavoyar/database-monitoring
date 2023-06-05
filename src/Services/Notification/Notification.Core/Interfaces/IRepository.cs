namespace DatabaseMonitoring.Services.Notification.Core.Interfaces;

public interface IRepository<T>
    where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> exspression = null);

    Task<T> GetFristOrDefaultAsync(Expression<Func<T, bool>> expression);

    Task<T> GetByIdAsync(string id);

    Task<string> CreateAsync(T entity);

    Task DeleteOneByIdAsync(string id);
    Task DeleteManyByIdAsync(IEnumerable<string> id);

}