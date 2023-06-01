using MIAUDataBase.DataBase.Entities;

namespace MIAUDataBase.Infrastructure.Repositories.Abstracts
{
    /// <summary>
    /// Нужен для DI. 
    /// </summary>
    public interface ILogRepository : IAbstractRepository<Log>
    {
        public List<Log> GetAllForServer(string id, int page, int itemsPerPage);
        public Task<List<Log>> GetAllForServerAsync(string id, int page, int itemsPerPage);

    }
}
