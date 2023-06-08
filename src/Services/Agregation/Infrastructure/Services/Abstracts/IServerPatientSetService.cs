using Agregation.Infrastructure.Services.DTO;
using MIAUDataBase.Services.Abstracts;

namespace Agregation.Infrastructure.Services.Abstracts
{
    /// <summary>
    /// Нужен для DI. 
    /// </summary>
    public interface IServerPatientSetService
    {
        public Task<List<ShortServerPatientDto>> GetShortServerPatientsPaged(int page, int itemsPerPage);
        public Task<ICollection<ServerPatientDto>> GetPagedAsync(int page, int itemsPerPage);
        public Task<ServerPatientDto> AddAsync(ServerPatientDto dto);
        public Task<bool> TryDeleteAsync(Guid id);
        public Task<ServerPatientDto?> GetAsync(Guid id);
        public Task<bool> TryUpdateAsync(ServerPatientDto dto);

    }
}
