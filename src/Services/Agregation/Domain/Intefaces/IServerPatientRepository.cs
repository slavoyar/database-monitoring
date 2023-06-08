using MIAUDataBase.DataBase.Entities;

namespace Agregation.Domain.Interfaces
{
    /// <summary>
    /// Нужен для DI. 
    /// </summary>
    public interface IServerPatientRepository : IAbstractRepository<ServerPatient>
    {
    }
}
