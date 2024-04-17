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
    }
}
