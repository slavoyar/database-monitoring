namespace MIAUDataBase.Controllers.Models.Log
{
    public class LogCreateFromServerPatientController
    {
        public string CriticalStatus { get; set; } = null!;
        public string ErrorState { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
        public string RecievedAt { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
