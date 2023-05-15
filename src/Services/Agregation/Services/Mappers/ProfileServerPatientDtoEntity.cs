using AutoMapper;
using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Mappers
{
    public class ProfileServerPatientDtoEntity : Profile
    {
        public ProfileServerPatientDtoEntity() 
        {
            CreateMap<ServerPatient, ServerPatientDto>();
            CreateMap<ServerPatientDto, ServerPatient>();
        }
    }
}
