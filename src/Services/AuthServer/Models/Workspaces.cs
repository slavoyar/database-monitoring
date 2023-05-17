using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class Workspaces : WorkspacesTeamsInitModel
    {
        [Key]
        public string? WorkspacesId { get; set; }

        public virtual List<AuthUser> Users { get; set; } = new List<AuthUser>();
    }
}