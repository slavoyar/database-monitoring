namespace DatabaseMonitoring.Services.Workspace.Core.Abstraction;

public interface IUnitOfWork
{
    public IRepository<Server> Servers { get; }
    public IRepository<User> Users { get; }
    public IRepository<WorkspaceEntity> Workspaces { get; }

}