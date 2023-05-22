using AutoMapper;
using MIAUDataBase.Controllers.Models.Log;
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
        public ServerPatientController( ILogSetService logSetService,
            IServerPatientSetService setService, IMapper mapper) : base(setService, mapper)
        {
            this.logSetService = logSetService;
        }

        /*[HttpPost("{serverId}")]
        public async Task<IResult> Create(string serverId, LogCreateFromServerPatientController logModel)
        {
            var modelToCreate = mapper.Map<LogCreateModel>(logModel) ?? throw new Exception("Problem after mapping");
            modelToCreate.ServerId = serverId;
            var dto = mapper.Map<LogDto>(modelToCreate);
            var retObj = await logSetService.AddAsync(dto);
            var retModel = mapper.Map<LogViewModel>(retObj);
            return Results.Created("Not uri", retModel);
        }*/




    }
}
