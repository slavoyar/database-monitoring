﻿using AutoMapper;
using MIAUDataAgregation.Infrastructure.Services.DTO;
using MIAUDataBase.Controllers.Models.ServerPatient;

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
