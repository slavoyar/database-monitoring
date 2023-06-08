using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;
using AutoMapper;
using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;

namespace Agregation.Infrastructure.Services.Implementations
{
    public class LogSetService : AbstractSetService<LogDto, Log>, ILogSetService
    {
        private readonly ILogRepository logRepository;
        public LogSetService(ILogRepository repository, IMapper mapper) : base(repository, mapper)
        {
            logRepository = repository;
        }
        public async Task<List<LogDto>> GetAllForServerAsync(string serverId, int page, int itemsPerPage)
        {
            var entities = await logRepository.GetAllForServerAsync(serverId, page, itemsPerPage);
            var dtos = mapper.Map<ICollection<LogDto>>(entities);
            return dtos.ToList();
        }

        public async Task<int> GetNumberOfLogsById(string serverPatientId, int page, int itemsPerPage)
        {
            var count = await logRepository.GetNumberOfLogsByIdAsync(serverPatientId, page, itemsPerPage);
            return count;
        }
    }
}
