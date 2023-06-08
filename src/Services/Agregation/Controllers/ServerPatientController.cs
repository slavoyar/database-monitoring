using AutoMapper;
using MIAUDataBase.Controllers.Models.ServerPatient;
using MIAUDataBase.Services.Abstracts;
using MIAUDataBase.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MIAUDataBase.Controllers
{
    public class ServerPatientController :
        AbstractController<
            ServerPatientCreateModel, ServerPatientEditModel, ServerPatientViewModel, ServerPatientDto>
    {
        protected readonly ILogSetService logSetService;
        protected readonly IServerPatientSetService serverPatientSetService;
        public ServerPatientController( ILogSetService logSetService,
            IServerPatientSetService setService, IMapper mapper) : base(setService, mapper)
        {
            this.logSetService = logSetService;
            serverPatientSetService = setService;
        }

        [HttpGet("shortList/{page}/{itemsPerPage}")]
        public async Task<IResult> GetShortListByPage(int page, int itemsPerPage)
        {
            if (page <= 0 || itemsPerPage <= 0) return Results.ValidationProblem(new Dictionary<string, string[]>() {
                    { "page or items per page less or equal then 0" , new string[]{ "Enter correct numbers" }  },
                });
            var dtoPage = await serverPatientSetService.GetShortServerPatientsPaged(page, itemsPerPage);
            var viewModelPage = mapper.Map<ICollection<ServerPatientShortViewModel>>(dtoPage);
            return Results.Ok(viewModelPage);
        }
    }
}
