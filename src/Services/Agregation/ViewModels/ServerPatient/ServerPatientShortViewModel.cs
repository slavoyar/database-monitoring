namespace MIAUDataBase.Controllers.Models.ServerPatient
{
    public class ServerPatientShortViewModel
    {
        public string Name { get; set; } = null!;
        public string IdAddress { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int CountOfLogs { get; set; }
    }
}
