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
    }
}
