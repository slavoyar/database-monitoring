namespace DatabaseMonitoring.Services.Workspace.Core.Abstraction;

/// <summary>
/// Base intefrace of unit of work
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Servers
    /// </summary>
    public IRepository<Server> Servers { get; }

    /// <summary>
    /// Users
    /// </summary>
    public IRepository<User> Users { get; }

    /// <summary>
    /// Workspaces
    /// </summary>
    public IRepository<WorkspaceEntity> Workspaces { get; }

}