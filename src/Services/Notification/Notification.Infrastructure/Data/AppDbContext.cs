namespace DatabaseMonitoring.Services.Notification.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<MailEntity> EmailMessages { get; set; }
    public DbSet<ErrorSending> ErrorSendings { get; set; }
}