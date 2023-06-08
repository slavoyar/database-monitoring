﻿using Agregation.ViewModels.LogModels;
using AutoMapper;

namespace Agregation.Controllers.Mappers
{
    public class ProfileLogModels : Profile
    {
        public ProfileLogModels() 
        {
            CreateMap<LogCreateFromServerPatientController, LogCreateModel>()
                .ForMember(d => d.ServerId, map => map.Ignore());
        }
    }
}
