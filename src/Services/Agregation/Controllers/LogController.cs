using AutoMapper;
using MIAUDataBase.Controllers.Models.Log;
using MIAUDataBase.Services.Abstracts;
using MIAUDataBase.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MIAUDataBase.Controllers
{
    public class LogController :
        AbstractController<LogCreateModel, LogEditModel, LogViewModel, LogDto>
    {

        public LogController(ILogSetService setService, IMapper mapper) : base(setService, mapper)
        {
        }

        [HttpGet("Logs/{serverid}")]
        public async Task<IResult> DeleteById(Guid id, int itemsPerPage, int page)
        {
            var result = await setService.TryDeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        }
    }
}
