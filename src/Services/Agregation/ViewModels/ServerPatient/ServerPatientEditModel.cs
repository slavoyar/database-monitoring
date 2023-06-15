namespace Agregation.ViewModels.ServerPatient
{
    public class ServerPatientEditModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public bool PingStatus { get; set; }
        public bool ConnectionStatus { get; set; }
        public string IdAddress { get; set; } = null!;
        public DateTime LastSuccessLog { get; set; } = DateTime.Now;
        public string IconId { get; set; } = null!;
    }
}
