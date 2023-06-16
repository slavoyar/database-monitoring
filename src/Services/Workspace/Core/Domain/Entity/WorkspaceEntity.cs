namespace DatabaseMonitoring.Services.Workspace.Core.Domain.Entity;

/// <summary>
/// Workspace model
/// </summary>
public class WorkspaceEntity : BaseEntity
{
    /// <summary>
    /// Name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [MaxLength(250)]
    public string Description { get; set; }

    /// <summary>
    /// Users
    /// </summary>
    /// <returns></returns>
    public ICollection<User> Users { get; set; }

    /// <summary>
    /// Servers
    /// </summary>
    /// <returns></returns>
    public ICollection<Server> Servers { get; set; }
}