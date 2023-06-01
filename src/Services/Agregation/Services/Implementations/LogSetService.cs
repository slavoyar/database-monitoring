using AutoMapper;
using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using MIAUDataBase.Services.Abstracts;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Implementations
{
    public class LogSetService : AbstractSetService<LogDto, Log>, ILogSetService
    {
        private readonly ILogRepository logRepository;
        public LogSetService(ILogRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this.logRepository = repository;
        }
        public async Task<List<LogDto>> GetAllForServerAsync(string serverId, int page, int itemsPerPage)
        {
            var entities = await logRepository.GetAllForServerAsync(serverId, page, itemsPerPage);
            var dtos = mapper.Map<ICollection<LogDto>>(entities);
            return dtos.ToList();
        }        
    }
}
