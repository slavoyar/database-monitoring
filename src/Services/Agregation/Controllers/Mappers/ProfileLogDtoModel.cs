using AutoMapper;
using MIAUDataAgregation.Infrastructure.Services.DTO;
using MIAUDataBase.Controllers.Models.Log;

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
