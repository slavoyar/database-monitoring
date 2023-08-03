namespace DatabaseMonitoring.Services.Workspace.Core.Domain.Entity;

/// <summary>
/// User model
/// </summary>
public class User : BaseEntity, IWithOuterId
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