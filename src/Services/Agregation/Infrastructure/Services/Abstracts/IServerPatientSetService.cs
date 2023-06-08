using Agregation.Infrastructure.Services.DTO;
using MIAUDataBase.Services.Abstracts;

namespace Agregation.Infrastructure.Services.Abstracts
{
    /// <summary>
    /// Нужен для DI. 
    /// </summary>
    public interface IServerPatientSetService : ISetService<ServerPatientDto>
    {
        public Task<List<ShortServerPatientDto>> GetShortServerPatientsPaged(int page, int itemsPerPage);
    }
}
