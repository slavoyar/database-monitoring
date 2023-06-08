﻿using System.ComponentModel.DataAnnotations;

namespace Agregation.Domain.Models
{
    public class ServerPatient : AbstractEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Status { get; set; } = null!;
        [Required]
        public bool PingStatus { get; set; }
        [Required]
        public bool ConnectionStatus { get; set; }
        [Required]
        public string IdAddress { get; set; } = null!;
        [Required]
        public string LastSuccessLog { get; set; } = null!;
        [Required]
        public string IconId { get; set; } = null!;

    }
}
