namespace DatabaseMonitoring.Services.Workspace.Infrustructure.Repository;

/// <summary>
/// Unit of work implementation
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Servers
    /// </summary>
    public IRepository<Server> Servers { get; private set; }

    /// <summary>
    /// Servers
    /// </summary>
    public IRepository<User> Users { get; private set; }

    /// <summary>
    /// Servers
    /// </summary>
    public IRepository<WorkspaceEntity> Workspaces { get; private set; }

    /// <summary>
    /// Contructor
    /// </summary>
    public UnitOfWork(
        IRepository<Server> servers,
        IRepository<User> users,
        IRepository<WorkspaceEntity> workspaces
        )
    {
        Servers = servers;
        Users = users;
        Workspaces = workspaces;
    }
}