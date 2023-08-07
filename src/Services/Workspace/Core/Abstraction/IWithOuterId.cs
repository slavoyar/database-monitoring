namespace DatabaseMonitoring.Services.Workspace.Core.Abstraction;

/// <summary>
/// Interface for models with outer id
/// </summary>
/// <returns></returns>
public interface IWithOuterId
{
    /// <summary>
    /// Outer id
    /// </summary>
    public Guid OuterId {get; set;}

    /// <summary>
    /// Workspace
    /// </summary>
    public WorkspaceEntity Workspace { get; set; }
}