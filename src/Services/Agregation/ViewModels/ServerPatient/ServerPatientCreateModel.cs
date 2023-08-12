namespace Agregation.ViewModels.ServerPatient
{
    public class ServerPatientCreateModel
    {
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public bool PingStatus { get; set; } = false;
        public bool ConnectionStatus { get; set; } = false;
        public string IdAddress { get; set; } = null!;
        public DateTime LastSuccessLog { get; set; } = DateTime.Now;
        public string IconId { get; set; } = null!;

    }
}
