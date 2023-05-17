﻿using AutoMapper;
using MIAUDataBase.Controllers.Models.Log;

namespace MIAUDataBase.Controllers.Mappers
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