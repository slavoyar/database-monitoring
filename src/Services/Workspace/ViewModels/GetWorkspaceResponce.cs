namespace DatabaseMonitoring.Services.Workspace.ViewModels;

/// <summary>
/// Get worksapce responce
/// </summary>
public class GetWorkspaceResponce
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
}