using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Abstracts
{
    /// <summary>
    /// Нужен для DI. 
    /// </summary>
    public interface IServerPatientSetService : ISetService<ServerPatientDto>
    {
    }
}
