using Agregation.Infrastructure.Services.DTO;

namespace MIAUDataBase.Services.Abstracts
{
    /// <summary>
    /// Нужен для DI
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public interface ISetService<TDto> where TDto : IDto
    {
        #region Get
        public Task<TDto?> GetAsync(Guid id);
        public Task<ICollection<TDto>> GetPagedAsync(int page, int itemsPerPage);
        #endregion
        #region Add
        public Task<TDto> AddAsync(TDto dto);
        public Task AddRangeAsync(ICollection<TDto> entities);
        #endregion
        #region Update
        public Task<bool> TryUpdateAsync(TDto dto);
        #endregion
        #region Delete
        public Task<bool> TryDeleteAsync(Guid id);
        #endregion
    }
}
