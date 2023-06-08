using Agregation.Infrastructure.Services.DTO;
using Agregation.ViewModels.LogModels;
using AutoMapper;

namespace Agregation.Controllers.Mappers
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
