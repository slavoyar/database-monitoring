using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace MIAUDataBase.Infrastructure.Repositories.Implementation
{
    public class LogRepository : AbstractRepository<Log>, ILogRepository
    {
        public LogRepository(DbContext context) : base(context)
        {
        }
    }
}
