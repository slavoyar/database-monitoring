namespace Agregation.Infrastructure.Services.DTO
{
    /// <summary>
    /// Короткое описание сервера пациента
    /// </summary>
    public class ShortServerPatientDto
    {
        public string Name { get; set; } = null!;
        public string IdAddress { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int CountOfLogs { get; set; }
    }
}
