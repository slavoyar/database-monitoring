namespace Agregation.Infrastructure.Services.DTO
{
    /// <summary>
    /// Short description of server patient
    /// </summary>
    public class ShortServerPatientDto
    {
        public Guid Id { get; set;}
        public string Name { get; set; } = null!;
        public string IdAddress { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int CountOfLogs { get; set; }
    }
}
