using System.ComponentModel.DataAnnotations;

namespace Agregation.Domain.Models
{
    public class ServerPatient : AbstractEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public bool PingStatus { get; set; }

        [Required]
        public bool ConnectionStatus { get; set; }

        [Required]
        public string ConnectionAddress { get; set; } = null!;

        public DateTime LastSuccessLog { get; set; } = DateTime.Now;

        public string IconName { get; set; } = null!;

        ICollection<Log> Logs { get; set; } = null!;
    }
}
