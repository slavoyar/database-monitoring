using Agregation.Infrastructure.Services.Abstracts;
using Agregation.ViewModels.LogModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Agregation.Controllers
{
    [Route("[controller]")]
    public class LogController : Controller
    {
        protected readonly ILogSetService logSetService;
        protected readonly IMapper mapper;
        public LogController(ILogSetService setService, IMapper mapper)
        {
            logSetService = setService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить логи сервера
        /// </summary>
        /// <param name="serverId">Идентификатор сервера</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="itemsPerPage">Количесвто логов на страницу</param>
        /// <returns>Возвращает список логов данного сервера в пагинированном виде</returns>
        [HttpGet("{serverId}/{page}/{itemsPerPage}")]
        public async Task<IResult> GetAllById(string serverId, int page, int itemsPerPage)
        {
            var dtos = await logSetService.GetAllForServerAsync(serverId, page, itemsPerPage);
            var models = mapper.Map<ICollection<LogViewModel>>(dtos);
            return Results.Ok(models);
        }
    }
}
