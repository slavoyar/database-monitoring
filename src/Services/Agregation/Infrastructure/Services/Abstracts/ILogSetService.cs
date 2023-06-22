using Agregation.Infrastructure.Services.DTO;

namespace Agregation.Infrastructure.Services.Abstracts
{
    /// <summary>
    /// Used in DI. 
    /// </summary>
    public interface ILogSetService
    {
        public Task<List<LogDto>> GetAllForServerAsync(string serverId, int page, int itemsPerPage);
        public Task<int> GetNumberOfLogsById(string serverPatientId, int page, int itemsPerPage);

    }
}
