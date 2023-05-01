using System.Linq.Expressions;
using DatabaseMonitoring.Services.Notification.Core.Models;

namespace DatabaseMonitoring.Services.Notification.Core.Interfaces;

public interface IRepository<T>
    where T: BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> exspression = null);

    Task<T> GetFristOrDefaultAsync(Expression<Func<T, bool>> expression);
    
    Task<T> GetByIdAsync(Guid id);

    Task<T> CreateAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<T> DeleteAsync(T entity);

    Task<bool> SaveChangesAsync();
}