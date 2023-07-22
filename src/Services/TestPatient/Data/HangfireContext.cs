using Microsoft.EntityFrameworkCore;
using TestPatient.Models;

namespace TestPatient.Data
{
    public class HangfireContext : DbContext
    {
        public HangfireContext(DbContextOptions<HangfireContext> options)
            : base(options)
        {
        }

        public DbSet<LogModel> PatientLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}