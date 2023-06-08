using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;
using Agregation.ViewModels.LogModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Agregation.Controllers
{
    [Route("[controller]")]
    public class LogController : Controller// AbstractController<LogCreateModel, LogEditModel, LogViewModel, LogDto>
    {
        protected readonly ILogSetService logSetService;
        protected readonly IMapper mapper;
        public LogController(ILogSetService setService, IMapper mapper)// : base(setService, mapper)
        {
            logSetService = setService;
            this.mapper = mapper;
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
