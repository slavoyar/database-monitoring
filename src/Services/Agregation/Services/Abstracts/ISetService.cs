using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Abstracts
{
    public interface ISetService<T> where T : AbstractDto
    {
        #region Get
        public Task<T?> GetAsync(Guid id);
        public Task<ICollection<T>> GetPagedAsync(int pageIndex, int pageSize);
        #endregion
        #region Add
        public Task<T> AddAsync(T dto);
        #endregion
        #region Update
        public Task<bool> TryUpdateAsync(T dto);
        #endregion
        #region Delete
        public Task<bool> TryDeleteAsync(Guid id);
        #endregion
    }
}
