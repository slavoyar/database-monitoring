using Agregation.Domain.Models;
using Agregation.Infrastructure.Services.DTO;

namespace Agregation.Domain.Interfaces
{
    /// <summary>
    /// Used in DI. 
    /// </summary>
    public interface IServerPatientRepository : IAbstractRepository<ServerPatient>
    {
    }
}
