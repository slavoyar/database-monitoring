using System.ComponentModel.DataAnnotations;

namespace TestPatient.Models
{
    public class LogModel
    {
        [Key]
        public string? Id { get; set; }

        public string? ServerId { get; set; }
        public string? CriticalStatus { get; set; }
        public string? ErrorState { get; set; }
        public string? ServiceType { get; set; }
        public string? ServiceName { get; set; }
        public string? CreatedAt { get; set; }
        public string? ReceivedAt { get; set; }
        public string? Message { get; set; }
    }
}