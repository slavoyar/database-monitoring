namespace DatabaseMonitoring.Services.Workspace.Infrustructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    public IRepository<Server> Servers { get; private set; }
    public IRepository<User> Users { get; private set; }
    public IRepository<WorkspaceEntity> Workspaces { get; private set; }

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