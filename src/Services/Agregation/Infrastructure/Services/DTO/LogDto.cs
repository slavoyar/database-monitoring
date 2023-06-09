﻿namespace Agregation.Infrastructure.Services.DTO
{
    public class LogDto : IDto
    {
        public string Id { get; set; } = null!;
        public string ServerId { get; set; } = null!;
        public string CriticalStatus { get; set; } = null!;
        public string ErrorState { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
        public string RecievedAt { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
