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
        public string IdAddress { get; set; } = null!;

        public DateTime LastSuccessLog { get; set; } = DateTime.Now;

        public string IconId { get; set; } = null!;

        public virtual List<Log> Logs { get; set; } = new List<Log>();
    }
}
