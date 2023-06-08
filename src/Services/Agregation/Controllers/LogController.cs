﻿using AutoMapper;
using MIAUDataAgregation.Infrastructure.Services.Abstracts;
using MIAUDataAgregation.Infrastructure.Services.DTO;
using MIAUDataBase.Controllers.Models.Log;
using Microsoft.AspNetCore.Mvc;

namespace MIAUDataBase.Controllers
{
    public class LogController : AbstractController<LogCreateModel, LogEditModel, LogViewModel, LogDto>
    {
         private readonly ILogSetService logSetService;
        public LogController(ILogSetService setService, IMapper mapper) : base(setService, mapper)
        {
            this.logSetService = setService;
        }

        [HttpGet("byServerId/{serverId}")]
        public async Task<IResult> GetAllById(string serverId, int page, int itemsPerPage)
        {
            var dtos = await logSetService.GetAllForServerAsync(serverId, page, itemsPerPage);
            var models = mapper.Map<ICollection<LogViewModel>>(dtos);
            return Results.Ok(models);
        }
    }
}
