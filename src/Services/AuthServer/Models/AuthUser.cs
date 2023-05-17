using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class AuthUser : IdentityUser
    {
        public string? FullUserName { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

        public string? Role { get; set; }

        [JsonIgnore]
        public override string? PasswordHash { get; set; }

        [JsonIgnore]
        public override string? SecurityStamp { get; set; }

        [JsonIgnore]
        public override string? ConcurrencyStamp { get; set; }

        public virtual List<Workspaces> Workspaces { get; set; } = new List<Workspaces>();
    }
}