namespace DatabaseMonitoring.Services.Workspace.Services.Contract;

/// <summary>
/// Worksapce data transfer object
/// </summary>
public class WorkspaceDto
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Users
    /// </summary>
    public ICollection<User> Users { get; set; }

    /// <summary>
    /// Servers
    /// </summary>
    public ICollection<Server> Servers { get; set; }

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreationDate { get; set; }
}