using Agregation.Domain.Models;
using Agregation.Infrastructure.Services.DTO;
using AutoMapper;

namespace Agregation.Infrastructure.Services.Mappers
{
    public class ProfileLogDtoEntity : Profile
    {
        public ProfileLogDtoEntity()
        {
            CreateMap<Log, LogDto>()
                .ForMember(d => d.ServerId, m => m.MapFrom(s => s.ServerPatientId.ToString()))
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(s => s.CreationDate.ToString()));
            CreateMap<LogDto, Log>()
                .ForMember(d => d.Id, m => m.MapFrom(s => Guid.Parse(s.Id)))
                .ForMember(d => d.CreationDate, m => m.MapFrom(s => DateTime.Parse(s.CreatedAt)))
                .ForMember(d => d.ServerPatientId, m => m.MapFrom(s => Guid.Parse(s.ServerId)))
                .ForMember(d => d.ServerPatient, m => m.Ignore());
        }
    }
}
