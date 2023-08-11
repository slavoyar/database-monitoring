using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Agregation.Domain.Intefaces;

namespace Agregation.Domain.Models
{
    /// <summary>
    /// Log for the server patient
    /// </summary>
    public class Log : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string CriticalStatus { get; set; } = null!;

        [Required]
        public string ErrorState { get; set; } = null!;

        [Required]
        public string ServiceType { get; set; } = null!;

        public string ServiceName { get; set; } = null!;

        public string CreationDate { get; set; } = null!;

        public string RecievedAt { get; set; } = DateTime.Now.ToString();

        public string Message { get; set; } = null!;

        public Guid ServerPatientId { get; set; }

        [ForeignKey("ServerPatientId")]
        public virtual ServerPatient ServerPatient { get; set; } = null!;

    }
}
