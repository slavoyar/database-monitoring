namespace DatabaseMonitoring.Services.Workspace.Services.Contract;

/// <summary>
/// Workspace data transfer object
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
    public ICollection<Guid> Users { get; set; }

    /// <summary>
    /// Servers
    /// </summary>
    public ICollection<Guid> Servers { get; set; }

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTimeOffset CreationDate { get; set; }
}