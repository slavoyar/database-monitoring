using Agregation.Domain.Interfaces;
using Agregation.Domain.Models;
using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;
using Agregation.ViewModels.ServerPatient;
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
            for (int i = 0; i < serverEntities.Count; i++)
            {
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

        public async Task<ServerPatientDto> AddAsync(ServerPatientEditModel model)
        {
            var server = new ServerPatient
            {
                Id = model.Id,
                Name = model.Name,
                IdAddress = model.IdAddress,
                Status = "Down",
                PingStatus = false,
                ConnectionStatus = false,
                IconId = "1",
            };
            var entityToReturn = await serverPatientRepository.AddAsync(server);
            await serverPatientRepository.SaveChangesAsync();
            var dtoToReturn = mapper.Map<ServerPatientDto>(entityToReturn);
            return dtoToReturn;
        }

        public async Task<bool> TryDeleteAsync(Guid id)
        {
            return await Task.Run(() =>
            {
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

        public async Task<ShortServerPatientDto?> GetShortAsync(Guid id)
        {
            var entity = await serverPatientRepository.GetAsync(id);
            var dto = mapper.Map<ShortServerPatientDto>(entity);
            return dto;
        }

        public async Task<ICollection<ServerPatientDto>?> GetListByListGuid(ICollection<Guid> guids)
        {
            var entity = await Task.Run(() => serverPatientRepository.GetListGuid(guids));
            var dto = mapper.Map<ICollection<ServerPatientDto>>(entity);
            return dto;
        }

        public async Task<ICollection<ShortServerPatientDto>?> GetShortListByListGuid(ICollection<Guid> guids)
        {
            var entity = await Task.Run(() => serverPatientRepository.GetListGuid(guids));
            var dto = mapper.Map<ICollection<ShortServerPatientDto>>(entity);
            return dto;
        }

        public async Task<bool> TryUpdateAsync(ServerPatientEditModel editModel)
        {
            var server = serverPatientRepository.Get(editModel.Id);
            if (server == null)
                return false;

            server.Name = editModel.Name;
            server.IdAddress = editModel.IdAddress;
            var result = serverPatientRepository.TryUpdate(server);
            await serverPatientRepository.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateStatusAsync(ShortServerPatientDto model)
        {
            var server = serverPatientRepository.Get(model.Id);
            if (server == null)
                return false;

            server.Status = model.Status;
            var result = serverPatientRepository.TryUpdate(server);
            await serverPatientRepository.SaveChangesAsync();
            return result;
        }
    }
}
