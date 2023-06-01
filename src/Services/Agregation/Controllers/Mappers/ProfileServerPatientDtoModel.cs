using AutoMapper;
using MIAUDataBase.Controllers.Models.Log;
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
        }
    }
}
