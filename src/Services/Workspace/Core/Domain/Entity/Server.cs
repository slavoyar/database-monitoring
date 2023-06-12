namespace DatabaseMonitoring.Services.Workspace.Core.Domain.Entity;

/// <summary>
/// Server model
/// </summary>
public class Server : BaseEntity
{

    /// <summary>
    /// Outer identifier
    /// </summary>
    public Guid OuterId { get; set; }
    /// <summary>
    /// Workspace
    /// </summary>
    public WorkspaceEntity Workspace { get; set; }
}