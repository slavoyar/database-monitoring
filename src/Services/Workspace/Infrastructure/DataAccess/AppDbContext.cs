namespace DatabaseMonitoring.Services.Workspace.Infrustructure.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<WorkspaceEntity> Workspaces { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Server> Servers { get; set; }
}