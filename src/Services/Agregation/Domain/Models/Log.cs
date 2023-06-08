using System.ComponentModel.DataAnnotations;

namespace Agregation.Domain.Models
{
    /// <summary>
    /// Лог для сервера пациента
    /// </summary>
    public class Log : AbstractEntity
    {
        [Required]
        public string ServerId { get; set; } = null!;
        [Required]
        public string CriticalStatus { get; set; } = null!;
        [Required]
        public string ErrorState { get; set; } = null!;
        [Required]
        public string ServiceType { get; set; } = null!;
        [Required]
        public string ServiceName { get; set; } = null!;
        [Required]
        public string CreatedAt { get; set; } = null!;
        [Required]
        public string RecievedAt { get; set; } = null!;
        [Required]
        public string Message { get; set; } = null!;
    }
}
