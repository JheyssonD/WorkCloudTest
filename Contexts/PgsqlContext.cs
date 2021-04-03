using Microsoft.EntityFrameworkCore;
using WorkCloudTest.Entities;

namespace WorkCloudTest.Contexts
{
    public class PgsqlContext : DbContext
    {
        private readonly DbContextOptions _options;

        public PgsqlContext(DbContextOptions<PgsqlContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
