using Agregation.Domain.Interfaces;
using Agregation.Domain.Models;
using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;
using AutoMapper;

namespace Agregation.Infrastructure.Services.Implementations
{
    public class ServerPatientSetService : IServerPatientSetService
    {
        protected readonly ILogRepository logRepository;
        protected readonly IServerPatientRepository serverPatientRepository;
        protected readonly IMapper mapper;
        public ServerPatientSetService(
            ILogRepository _logRepository, IServerPatientRepository repository, IMapper mapper)
        {
            logRepository = _logRepository;
            serverPatientRepository = repository;   
            this.mapper = mapper;
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

        public async Task<ICollection<ServerPatientDto>> GetPagedAsync(int page, int itemsPerPage)
        {
            var entities = await serverPatientRepository.GetPagedAsync(page, itemsPerPage);
            return mapper.Map<ICollection<ServerPatientDto>>(entities);
        }

        public async Task<ServerPatientDto> AddAsync(ServerPatientDto dto)
        {
            var entity = mapper.Map<ServerPatient>(dto);
            var entityToReturn = await serverPatientRepository.AddAsync(entity);
            await serverPatientRepository.SaveChangesAsync();
            var dtoToReturn = mapper.Map<ServerPatientDto>(entityToReturn);
            return dtoToReturn;
        }

        public async Task<bool> TryDeleteAsync(Guid id)
        {
            return await Task.Run(() => {
                var isSuccess = serverPatientRepository.TryDelete(id);
                serverPatientRepository.SaveChanges();
                return isSuccess;
            });
        }

        public async Task<ServerPatientDto?> GetAsync(Guid id)
        {
            var entity = await serverPatientRepository.GetAsync(id);
            var dto = mapper.Map<ServerPatientDto>(entity);
            return dto;
        }

        public async Task<bool> TryUpdateAsync(ServerPatientDto dto)
        {
            var entity = mapper.Map<ServerPatient>(dto);
            return await Task.Run(() => {
                var isSuccess = serverPatientRepository.TryUpdate(entity);
                serverPatientRepository.SaveChanges();
                return isSuccess;
            });
        }
    }
}
