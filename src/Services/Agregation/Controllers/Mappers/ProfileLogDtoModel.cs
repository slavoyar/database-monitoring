using AutoMapper;
using MIAUDataBase.Controllers.Models.Log;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Controllers.Mappers
{
    public class ProfileLogDtoModel : Profile
    {
        public ProfileLogDtoModel() 
        {
            CreateMap<LogDto, LogViewModel>();
            CreateMap<LogEditModel, LogDto>();
            CreateMap<LogCreateModel, LogDto>()
                .ForMember(d => d.Id, map => map.Ignore());
        }
    }
}
