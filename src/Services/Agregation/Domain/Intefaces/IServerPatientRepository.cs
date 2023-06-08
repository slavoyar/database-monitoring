using Agregation.Domain.Models;

namespace Agregation.Domain.Interfaces
{
    /// <summary>
    /// Нужен для DI. 
    /// </summary>
    public interface IServerPatientRepository : IAbstractRepository<ServerPatient>
    {
    }
}
