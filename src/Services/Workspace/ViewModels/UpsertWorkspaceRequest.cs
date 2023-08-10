namespace DatabaseMonitoring.Services.Workspace.ViewModels;

/// <summary>
/// Create workspace request
/// </summary>
public class UpsertWorkspaceRequest
{
    /// <summary>
    /// Required name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Description with max length of 250
    /// </summary>
    [MaxLength(250)]
    public string Description { get; set; }

    /// <summary>
    /// Users
    /// </summary>
    public ICollection<Guid> Users { get; set; }

    /// <summary>
    /// Servers
    /// </summary>
    public ICollection<Guid> Servers { get; set; }
}