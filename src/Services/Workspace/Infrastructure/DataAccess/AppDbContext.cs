namespace DatabaseMonitoring.Services.Workspace.Infrustructure.DataAccess;

/// <summary>
/// Database context
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Database context contructor
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    /// <summary>
    /// Workspaces
    /// </summary>
    public DbSet<WorkspaceEntity> Workspaces { get; set; }

    /// <summary>
    /// Workspaces
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Workspaces
    /// </summary>
    public DbSet<Server> Servers { get; set; }
}