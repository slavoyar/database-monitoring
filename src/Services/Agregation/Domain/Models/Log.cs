using System.ComponentModel.DataAnnotations;

namespace Agregation.Domain.Models
{
    /// <summary>
    /// Лог для сервера пациента
    /// </summary>
    public class Log : AbstractEntity
    {

        public bool CriticalStatus { get; set; }

        [Required]
        public string ErrorState { get; set; } = null!;

        [Required]
        public string ServiceType { get; set; } = null!;

        public string ServiceName { get; set; } = null!;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string RecievedAt { get; set; } = null!;

        public string Message { get; set; } = null!;

        public ServerPatient Server { get; set; } = null!;
    }
}
