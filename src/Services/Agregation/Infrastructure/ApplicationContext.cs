using MIAUDataBase.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace MIAUDataBase.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Log> Logs { get; set; } = null!;
        public DbSet<ServerPatient> ServerPatients { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
