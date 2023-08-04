using Agregation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Agregation.Infrastructure.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Log> Logs { get; set; } = null!;
        public DbSet<ServerPatient> ServerPatients { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedServers(modelBuilder);
        }

        private static void SeedServers(ModelBuilder builder)
        {
            builder.Entity<ServerPatient>().HasData(
                new ServerPatient()
                {
                    Id = new Guid("d69cd87f-1f08-4b12-af16-980b003cdc5f"),
                    Name = "testpatient1",
                    Status = "Working",
                    PingStatus = true,
                    ConnectionStatus = true,
                    IdAddress = "testpatient-1",
                    IconId = "1",
                },
                new ServerPatient()
                {
                    Id = new Guid("d13920a2-4961-43cc-bd22-12187b19f512"),
                    Name = "testpatient2",
                    Status = "Working",
                    PingStatus = true,
                    ConnectionStatus = true,
                    IdAddress = "testpatient-2",
                    IconId = "1",
                },
                new ServerPatient()
                {
                    Id = new Guid("8d8a6029-676a-4e09-91c5-32c56602f67f"),
                    Name = "testpatient3",
                    Status = "Working",
                    PingStatus = true,
                    ConnectionStatus = true,
                    IdAddress = "testpatient-3",
                    IconId = "1",
                }
                );
        }
    }
}
