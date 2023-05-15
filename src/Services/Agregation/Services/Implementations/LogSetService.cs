using AutoMapper;
using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using MIAUDataBase.Services.Abstracts;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Implementations
{
    public class LogSetService : AbstractSetService<LogDto, Log>, ILogSetService
    {
        public LogSetService(ILogRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
