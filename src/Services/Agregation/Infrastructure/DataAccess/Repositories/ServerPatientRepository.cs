using Agregation.Domain.Interfaces;
using Agregation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Agregation.Infrastructure.DataAccess.Repositories
{
    public class ServerPatientRepository : AbstractRepository<ServerPatient>, IServerPatientRepository
    {
        public ServerPatientRepository(ApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Запросить все сущности в базе (Список GUID)
        /// </summary>
        /// <param name="guids">Список Guid</param>
        /// <returns>IQueryable массив сущностей</returns>
        public IQueryable<ServerPatient> GetListGuid(ICollection<Guid> guids)
        {
            var query = entitySet.Where(data => guids.Contains(data.Id)).Include(e => e.Logs);
            return query;
        }
    }
}
