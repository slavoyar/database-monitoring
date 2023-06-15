using System.ComponentModel.DataAnnotations;
using Agregation.Domain.Intefaces;

namespace Agregation.Domain.Models
{
    public class ServerPatient : IEntity
    {
        [Key]
        public Guid Id { get; set; }

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

        public string LastSuccessLog { get; set; } = DateTime.Now.ToString();

        public string IconId { get; set; } = null!;

        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
    }
}
