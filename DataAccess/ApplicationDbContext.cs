using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CallDetailRecord> CallDetailRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CallDetailRecord>()
                .Property(c => c.Cost)
                .HasColumnType("decimal(18,3)");
        }
    }
}
