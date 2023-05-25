using AutoMapper;
using MIAUDataBase.DataBase;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using MIAUDataBase.Services.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MIAUDataBase.Services.Abstracts
{
    public abstract class AbstractSetService<TDto, TEntity> : ISetService<TDto>
        where TDto : AbstractDto
        where TEntity : AbstractEntity
    {
        protected readonly IAbstractRepository<TEntity> repository;
        protected readonly IMapper mapper;
        public AbstractSetService(IAbstractRepository<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            var entityToReturn = await repository.AddAsync(entity);
            await repository.SaveChangesAsync();
            var dtoToReturn = mapper.Map<TDto>(entityToReturn);
            return dtoToReturn;
        }

        public async Task AddRangeAsync(ICollection<TDto> dtos)
        {
            var entities = mapper.Map<ICollection<TEntity>>(dtos);
            await repository.AddRangeAsync(entities);
            await repository.SaveChangesAsync();
            return;
        }


        public async Task<bool> TryDeleteAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                var isSuccess = repository.TryDelete(id);
                repository.SaveChanges();
                return isSuccess;
            });
        }

        public async Task<ICollection<TDto>> GetPagedAsync(int pageIndex, int pageSize)
        {
            var query = repository.GetAll();
            var entities = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return mapper.Map<ICollection<TDto>>(entities);
        }

        public async Task<TDto?> GetAsync(Guid id)
        {
            var entity = await repository.GetAsync(id);
            var dto = mapper.Map<TDto>(entity);
            return dto;
        }

        public async Task<bool> TryUpdateAsync(TDto dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            return await Task.Run(() => 
            { 
                var isSuccess = repository.TryUpdate(entity); 
                repository.SaveChanges();
                return isSuccess;
            });
        }
    }
}
