﻿namespace Agregation.Infrastructure.Services.DTO
{
    public class ServerPatientDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public bool PingStatus { get; set; }
        public bool ConnectionStatus { get; set; }
        public string IdAddress { get; set; } = null!;
        public string LastSuccessLog { get; set; } = null!;
        public string IconId { get; set; } = null!;
    }
}
