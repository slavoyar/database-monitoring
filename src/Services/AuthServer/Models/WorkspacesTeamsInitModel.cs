using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class WorkspacesTeamsInitModel
    {
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}