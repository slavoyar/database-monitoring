using Agregation.Infrastructure.Services.DTO;
using Agregation.ViewModels.ServerPatient;

namespace Agregation.Infrastructure.Services.Abstracts
{
    /// <summary>
    /// Used in DI. 
    /// </summary>
    public interface IServerPatientSetService
    {
        public Task<List<ShortServerPatientDto>> GetShortServerPatientsPaged(int page, int itemsPerPage);
        public Task<ICollection<ServerPatientDto>> GetPagedAsync(int page, int itemsPerPage);
        public Task<ServerPatientDto> AddAsync(ServerPatientEditModel model);
        public Task<bool> TryDeleteAsync(Guid id);
        public Task<ServerPatientDto?> GetAsync(Guid id);
        public Task<ShortServerPatientDto?> GetShortAsync(Guid id);
        public Task<ICollection<ServerPatientDto>?> GetListByListGuid(ICollection<Guid> guids);
        public Task<ICollection<ShortServerPatientDto>?> GetShortListByListGuid(ICollection<Guid> guids);
        public Task<bool> TryUpdateAsync(ServerPatientEditModel model);
        public Task<bool> UpdateStatusAsync(ShortServerPatientDto model);
    }
}
