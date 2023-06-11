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
    public ICollection<User> Users { get; set; }

    /// <summary>
    /// Servers
    /// </summary>
    public ICollection<Server> Servers { get; set; }

    /// <summary>
    /// Date of creation
    /// </summary>
    public DateTime CreationDate { get; set; }
}