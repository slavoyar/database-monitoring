using Agregation.Domain.Models;
using Agregation.Infrastructure.Services.DTO;
using AutoMapper;

namespace Agregation.Infrastructure.Services.Mappers
{
    public class ProfileServerPatientDtoEntity : Profile
    {
        public ProfileServerPatientDtoEntity() 
        {
            CreateMap<ServerPatient, ServerPatientDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.LastSuccessLog, m => m.MapFrom(s => s.LastSuccessLog.ToString()));
            CreateMap<ServerPatientDto, ServerPatient>()
                .ForMember(d => d.Id, m => m.MapFrom(s => Guid.Parse(s.Id)))
                .ForMember(d => d.LastSuccessLog, m => m.MapFrom(s => DateTime.Parse(s.LastSuccessLog)))
                .ForMember(d => d.Logs, m => m.MapFrom(s => new List<Log>()));
            CreateMap<ServerPatient, ShortServerPatientDto>()
                .ForMember(d => d.CountOfLogs, map => map.Ignore());
        }
    }
}
