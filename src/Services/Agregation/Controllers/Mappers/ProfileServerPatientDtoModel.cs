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
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Status, map => map.Ignore())
                .ForMember(d => d.PingStatus, map => map.Ignore())
                .ForMember(d => d.ConnectionStatus, map => map.Ignore())
                .ForMember(d => d.LastSuccessLog, map => map.Ignore())
                .ForMember(d => d.IconId, map => map.Ignore());
        }
    }
}
