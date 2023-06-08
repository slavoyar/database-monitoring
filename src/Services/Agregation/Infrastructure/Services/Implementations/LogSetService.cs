using Agregation.Domain.Interfaces;
using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;
using AutoMapper;

namespace Agregation.Infrastructure.Services.Implementations
{
    public class LogSetService : ILogSetService
    {
        protected readonly ILogRepository logRepository;
        protected readonly IMapper mapper;
        public LogSetService(ILogRepository repository, IMapper mapper)
        {
            logRepository = repository;
            this.mapper = mapper;
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
