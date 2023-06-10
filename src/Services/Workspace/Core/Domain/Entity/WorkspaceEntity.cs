namespace DatabaseMonitoring.Services.Workspace.Core.Domain.Entity;

public class WorkspaceEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [MaxLength(250)]
    public string Description { get; set; }

    public ICollection<User> Users { get; set; }
    public ICollection<Server> Servers { get; set; }
}