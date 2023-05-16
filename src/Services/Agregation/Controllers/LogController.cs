using AutoMapper;
using MIAUDataBase.Controllers.Models.Log;
using MIAUDataBase.Services.Abstracts;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Controllers
{
    public class LogController :
        AbstractController<LogCreateModel, LogEditModel, LogViewModel, LogDto>
    {
        public LogController(ILogSetService setService, IMapper mapper) : base(setService, mapper)
        {
        }
    }
}
