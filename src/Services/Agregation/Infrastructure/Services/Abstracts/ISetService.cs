using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;

namespace MIAUDataBase.Services.Abstracts
{
    public interface ISetService<T> where T : AbstractDto
    {
        #region Get
        public Task<T?> GetAsync(Guid id);
        public Task<ICollection<T>> GetPagedAsync(int page, int itemsPerPage);
        #endregion
        #region Add
        public Task<T> AddAsync(T dto);
        public Task AddRangeAsync(ICollection<T> entities);
        #endregion
        #region Update
        public Task<bool> TryUpdateAsync(T dto);
        #endregion
        #region Delete
        public Task<bool> TryDeleteAsync(Guid id);
        #endregion
    }
}
