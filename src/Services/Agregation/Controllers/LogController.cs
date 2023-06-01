using AutoMapper;
using MIAUDataBase.Controllers.Models.Log;
using MIAUDataBase.Services.Abstracts;
using MIAUDataBase.Services.DTO;
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
