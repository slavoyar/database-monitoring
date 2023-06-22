using Agregation.Domain.Models;

namespace Agregation.Domain.Interfaces
{
    /// <summary>
    /// Used in DI. 
    /// </summary>
    public interface IServerPatientRepository : IAbstractRepository<ServerPatient>
    {
    }
}
