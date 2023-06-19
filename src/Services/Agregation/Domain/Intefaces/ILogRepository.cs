
using Agregation.Domain.Models;

namespace Agregation.Domain.Interfaces
{
    /// <summary>
    /// Used in DI. 
    /// </summary>
    public interface ILogRepository : IAbstractRepository<Log>
    {
        public List<Log> GetAllForServer(string id, int page, int itemsPerPage);
        public Task<List<Log>> GetAllForServerAsync(string id, int page, int itemsPerPage);
        public int GetNumberOfLogsById(string serverPatientId, int page, int itemsPerPage);
        public Task<int> GetNumberOfLogsByIdAsync(string serverPatientId, int page, int itemsPerPage);

    }
}
