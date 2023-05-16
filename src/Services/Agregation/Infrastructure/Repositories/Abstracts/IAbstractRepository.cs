using MIAUDataBase.DataBase;

namespace MIAUDataBase.Infrastructure.Repositories.Abstracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"> AbstractEntity нужен для использования Id и указания, что это объект из бд </typeparam>
    public interface IAbstractRepository<T> where T : AbstractEntity
    {
        #region Get
        public T? Get(Guid id);
        public Task<T?> GetAsync(Guid id);
        public IQueryable<T> GetAll(bool asNoTracking = false);
        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);
        #endregion
        #region Add
        public T Add(T entity);
        public  Task<T> AddAsync(T entity);
        public void AddRange(List<T> entities);
        public Task AddRangeAsync(ICollection<T> entities);
        #endregion
        #region Update
        public bool TryUpdate(T entity);
        #endregion
        #region Delete
        public bool TryDelete(Guid id);
        public bool Delete(T entity);
        public bool DeleteRange(ICollection<T> entities);
        #endregion
        #region Save
        public void SaveChanges();
        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
        #endregion
    }
}
