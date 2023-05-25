using AutoMapper;
using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using MIAUDataBase.Services.Abstracts;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Implementations
{
    public class LogSetService : AbstractSetService<LogDto, Log>, ILogSetService
    {
        new private readonly ILogRepository repository;
        public LogSetService(ILogRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this.repository = repository;
        }
        public async Task<List<LogDto>> GetAllForServerAsync(string serverId, int itemsPerPage, int page)
        {
            var entities = await repository.GetAllForServerAsync(serverId, itemsPerPage, page);
            var dtos = mapper.Map<ICollection<LogDto>>(entities);
            return dtos.ToList();
        }        
    }
}
