using Agregation.Controllers.Models.ServerPatient;
using AutoMapper;
using MIAUDataAgregation.Services.DTO;
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
            CreateMap<ShortServerPatientDto, ServerPatientShortViewModel>();
        }
    }
}
