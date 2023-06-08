using Agregation.Domain.Models;
using Agregation.Infrastructure.Services.DTO;
using AutoMapper;

namespace Agregation.Infrastructure.Services.Mappers
{
    public class ProfileLogDtoEntity : Profile
    {
        public ProfileLogDtoEntity()
        {
            CreateMap<Log, LogDto>();
            CreateMap<LogDto, Log>();
        }
    }
}
