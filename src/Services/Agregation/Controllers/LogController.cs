using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;
using Agregation.ViewModels.LogModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Agregation.Controllers
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
