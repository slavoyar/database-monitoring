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
        }
    }
}