using AutoMapper;
using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Mappers
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
