using MIAUDataAgregation.Services.DTO;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Abstracts
{
    /// <summary>
    /// Нужен для DI. 
    /// </summary>
    public interface IServerPatientSetService : ISetService<ServerPatientDto>
    {
        public Task<List<ShortServerPatientDto>> GetShortServerPatientsPaged(int page, int itemsPerPage);
    }
}
