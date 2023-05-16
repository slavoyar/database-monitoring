using MIAUDataBase.DataBase.Entities;

namespace MIAUDataBase.Infrastructure.Repositories.Abstracts
{
    /// <summary>
    /// Нужен для DI. 
    /// </summary>
    public interface IServerPatientRepository : IAbstractRepository<ServerPatient>
    {
    }
}
