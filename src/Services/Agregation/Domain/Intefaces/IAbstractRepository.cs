
using Agregation.Domain.Intefaces;

namespace Agregation.Domain.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"> AbstractEntity is needed to use the Id and indicate that it is an object from the database </typeparam>
    public interface IAbstractRepository<T> where T : IEntity
    {
        #region Get
        public T? Get(Guid id);
        public Task<T?> GetAsync(Guid id);
        public IQueryable<T> GetAll(bool asNoTracking = false);
        public IQueryable<T> GetListGuid(ICollection<Guid> guids);
        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);
        public List<T> GetPaged(int page, int itemsPerPage);
        public Task<List<T>> GetPagedAsync(int page, int itemsPerPage);
        #endregion
        #region Add
        public T Add(T entity);
        public Task<T> AddAsync(T entity);
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
