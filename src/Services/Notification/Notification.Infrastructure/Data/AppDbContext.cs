using DatabaseMonitoring.Services.Notification.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMonitoring.Services.Notification.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<EmailMessage> EmailMessages { get; set; }
}