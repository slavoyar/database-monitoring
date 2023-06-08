using Agregation.Infrastructure.Services.DTO;
using AutoMapper;
using MIAUDataBase.DataBase.Entities;

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
