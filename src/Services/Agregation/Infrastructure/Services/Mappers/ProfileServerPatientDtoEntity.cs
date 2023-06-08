using Agregation.Infrastructure.Services.DTO;
using AutoMapper;
using MIAUDataBase.DataBase.Entities;

namespace Agregation.Infrastructure.Services.Mappers
{
    public class ProfileServerPatientDtoEntity : Profile
    {
        public ProfileServerPatientDtoEntity() 
        {
            CreateMap<ServerPatient, ServerPatientDto>();
            CreateMap<ServerPatientDto, ServerPatient>();
            CreateMap<ServerPatient, ShortServerPatientDto>()
                .ForMember(d => d.CountOfLogs, map => map.Ignore());
        }
    }
}
