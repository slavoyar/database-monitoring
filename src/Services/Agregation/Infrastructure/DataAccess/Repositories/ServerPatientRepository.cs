using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Agregation.Infrastructure.DataAccess.Repositories
{
    public class ServerPatientRepository : AbstractRepository<ServerPatient>, IServerPatientRepository
    {
        public ServerPatientRepository(DbContext context) : base(context)
        {
        }
    }
}
