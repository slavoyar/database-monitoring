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
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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

    /// <summary>
    /// Configurting entity dependencies
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<WorkspaceEntity>()
            .HasMany(w => w.Users)
            .WithOne(u => u.Workspace)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<WorkspaceEntity>()
            .HasMany(w => w.Servers)
            .WithOne(s => s.Workspace)
            .OnDelete(DeleteBehavior.Cascade);
    }
}