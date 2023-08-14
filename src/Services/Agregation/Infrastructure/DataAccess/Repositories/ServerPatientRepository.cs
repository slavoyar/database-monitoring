using Agregation.Domain.Interfaces;
using Agregation.Domain.Models;
using Agregation.Infrastructure.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace Agregation.Infrastructure.DataAccess.Repositories
{
    public class ServerPatientRepository : AbstractRepository<ServerPatient>, IServerPatientRepository
    {
        public ServerPatientRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
