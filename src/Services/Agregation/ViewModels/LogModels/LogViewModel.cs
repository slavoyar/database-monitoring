namespace Agregation.ViewModels.LogModels
{
    public class LogViewModel
    {
        public string Id { get; set; } = null!;
        public string ServerId { get; set; } = null!;
        public string CriticalStatus { get; set; } = null!;
        public string ErrorState { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime RecievedAt { get; set; } = DateTime.Now;
        public string Message { get; set; } = null!;
    }
}
