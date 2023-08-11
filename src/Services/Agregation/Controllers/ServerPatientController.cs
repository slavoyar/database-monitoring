using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;
using Agregation.ViewModels.ServerPatient;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Agregation.Controllers
{
    [Route("server")]
    public class ServerPatientController : Controller
    {
        protected readonly ILogSetService logSetService;
        protected readonly IServerPatientSetService serverPatientSetService;
        protected readonly IMapper mapper;
        public ServerPatientController(ILogSetService logSetService,
            IServerPatientSetService setService, IMapper mapper)
        {
            this.logSetService = logSetService;
            serverPatientSetService = setService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список полной информации о серверах пациентах в пагинированном виде.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="itemsPerPage">Количесвто элементов на странице.</param>
        /// <returns>Возвращает itemsPerPage серверов пациентов с полной информацией о них.</returns>
        [HttpGet("{page}/{itemsPerPage}")]
        public async Task<IResult> GetPaged(int page, int itemsPerPage)
        {
            if (page <= 0 || itemsPerPage <= 0) return Results.ValidationProblem(new Dictionary<string, string[]>() {
                    { "page or items per page less or equal then 0" , new string[]{ "Enter correct numbers" }  },
                });
            var dtoPage = await serverPatientSetService.GetPagedAsync(page, itemsPerPage);
            var viewModelPage = mapper.Map<ICollection<ServerPatientViewModel>>(dtoPage);
            return Results.Ok(viewModelPage);
        }

        /// <summary>
        /// Получить список полной информации о серверах пациентах (Список GUID)
        /// </summary>
        /// <param name="guids">Список GUID</param>
        /// <returns>Возвращвет список серверов с полной информацией о них</returns>
        [HttpGet]
        public async Task<IResult> GetListGuid(List<Guid> guids)
        {
            var dtoPage = await serverPatientSetService.GetListByListGuid(guids);
            var viewModelPage = mapper.Map<ICollection<ServerPatientViewModel>>(dtoPage);
            return Results.Ok(viewModelPage);
        }

        /// <summary>
        /// Получить список серверов с кратким описанием в пагинированном виде
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="itemsPerPage">Количество серверов на странице</param>
        /// <returns>Возвращвет список серверов с краткой информацией</returns>
        [HttpGet("short/{page}/{itemsPerPage}")]
        public async Task<ActionResult<ICollection<ServerPatientShortViewModel>>> GetShortListByPage(int page, int itemsPerPage)
        {
            if (page <= 0 || itemsPerPage <= 0) return Results.ValidationProblem(new Dictionary<string, string[]>() {
                    { "page or items per page less or equal then 0" , new string[]{ "Enter correct numbers" }  },
                });
            var dtoPage = await serverPatientSetService.GetShortServerPatientsPaged(page, itemsPerPage);
            var viewModelPage = mapper.Map<ICollection<ServerPatientShortViewModel>>(dtoPage);
            return viewModelPage;
        }

        /// <summary>
        /// Получить список серверов с кратким описанием (Список GUID)
        /// </summary>
        /// <param name="guids">Список GUID</param>
        /// <returns>Возвращвет список серверов с краткой информацией</returns>
        [HttpGet("short")]
        public async Task<IResult> GetShortListGuid(List<Guid> guids)
        {
            var dtoPage = await serverPatientSetService.GetShortListByListGuid(guids);
            var viewModelPage = mapper.Map<ICollection<ServerPatientShortViewModel>>(dtoPage);
            return Results.Ok(viewModelPage);
        }

        /// <summary>
        /// Создать новый сервер пациент.
        /// </summary>
        /// <param name="ServerCreateRequest">Информация о сервере пациенте для создания.</param>
        /// <returns>Возвращает информацию о сервере с его идентификатором.</returns>
        [HttpPost]
        public async Task<ActionResult<ServerPatientViewModel>> Create(ServerPatientCreateModel ServerCreateRequest)
        {
            var dto = mapper.Map<ServerPatientDto>(ServerCreateRequest);
            var retObj = await serverPatientSetService.AddAsync(dto);
            var retModel = mapper.Map<ServerPatientViewModel>(retObj);
            return Results.Created("Not uri", retModel);
        }

        /// <summary>
        /// Удалить сервер пациент.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Возвращает 200, при удалении, 404, если сервер с эти ид не найден.</returns>
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteById(Guid id)
        {
            var result = await serverPatientSetService.TryDeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        }

        /// <summary>
        /// Получить полную информацию о сервере по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Возвращает информацию о сервере.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerPatientViewModel>> GetById(Guid id)
        {
            var dto = await serverPatientSetService.GetAsync(id);
            if (dto == null)
            {
                return Results.NotFound();
            }
            var view = mapper.Map<ServerPatientViewModel>(dto);
            return Results.Ok(view);
        }

        /// <summary>
        /// Обновить информацию о сервере.
        /// </summary>
        /// <param name="ServerUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResult> Edit(ServerPatientEditModel ServerUpdateRequest)
        {
            var dto = mapper.Map<ServerPatientDto>(ServerUpdateRequest);
            var result = await serverPatientSetService.TryUpdateAsync(dto);
            return result ? Results.Ok() : Result.NotFound();
        }
    }
}