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
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    var fakeDataFactory = new FakeDataMockFactory();
        //    var serverPatient = fakeDataFactory.GenerateFakeEmptyServerPatient();
        //    modelBuilder.Entity<ServerPatient>().HasData(new ServerPatient[] { serverPatient });
        //    var logList = fakeDataFactory.GenerateFakeLogs(serverPatient);
        //    modelBuilder.Entity<Log>().HasData(logList);
        //}
    }
}
