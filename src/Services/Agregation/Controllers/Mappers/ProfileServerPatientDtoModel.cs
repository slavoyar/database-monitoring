using AutoMapper;
using MIAUDataAgregation.Controllers.Models.ServerPatient;
using MIAUDataBase.Controllers.Models.ServerPatient;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Controllers.Mappers
{
    public class ProfileServerPatientDtoModel : Profile
    {
        public ProfileServerPatientDtoModel() 
        {
            CreateMap<ServerPatientDto, ServerPatientViewModel>();
            CreateMap<ServerPatientEditModel, ServerPatientDto>();
            CreateMap<ServerPatientCreateModel, ServerPatientDto>()
                .ForMember(d => d.Id, map => map.Ignore());
            CreateMap<ServerPatientDto, ServerPatientShortViewModel>()
                .ForMember(d => d.CountOfLogs, map => map.Ignore());
        }
    }
}
