namespace MIAUDataBase.DataBase.Entities
{
    public class ServerPatient : AbstractEntity
    {
        public string Name { get; set; } = null!;
        public string? Status { get; set; }
        public bool? PingStatus { get; set; }
        public bool? ConnectionStatus { get; set; }
        public string IdAddress { get; set; } = null!;
        public string? LastSuccessLog { get; set; }
        public string? IconId { get; set; }

    }
}
