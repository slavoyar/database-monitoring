using AutoMapper;
using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using MIAUDataBase.Services.Abstracts;
using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Implementations
{
    public class ServerPatientSetService :
        AbstractSetService<ServerPatientDto, ServerPatient>, IServerPatientSetService
    {
        public ServerPatientSetService(IServerPatientRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
