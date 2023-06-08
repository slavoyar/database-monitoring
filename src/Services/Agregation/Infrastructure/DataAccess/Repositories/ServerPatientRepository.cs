using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace MIAUDataBase.Infrastructure.Repositories.Implementation
{
    public class ServerPatientRepository : AbstractRepository<ServerPatient>, IServerPatientRepository
    {
        public ServerPatientRepository(DbContext context) : base(context)
        {
        }
    }
}
