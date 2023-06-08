using Agregation.Domain.Interfaces;
using Agregation.Domain.Models;
using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;
using AutoMapper;

namespace Agregation.Infrastructure.Services.Implementations
{
    public class ServerPatientSetService :
        AbstractSetService<ServerPatientDto, ServerPatient>, IServerPatientSetService
    {
        protected readonly ILogRepository logRepository;
        protected readonly IServerPatientRepository serverPatientRepository;
        public ServerPatientSetService(ILogRepository _logRepository, IServerPatientRepository repository, IMapper mapper) : base(repository, mapper)
        {
            logRepository = _logRepository;
            serverPatientRepository = repository;   
        }

        public async Task<List<ShortServerPatientDto>> GetShortServerPatientsPaged(int page, int itemsPerPage)
        {
            var serverEntities = await serverPatientRepository.GetPagedAsync(page, itemsPerPage);
            var shortServersPatients = mapper.Map<List<ShortServerPatientDto>>(serverEntities);
            for (int i = 0; i < serverEntities.Count; i++) {
                var count = logRepository.GetNumberOfLogsById(serverEntities[i].Id.ToString(), page, itemsPerPage);
                shortServersPatients[i].CountOfLogs = count;
            }
            return shortServersPatients;
        }
    }
}
