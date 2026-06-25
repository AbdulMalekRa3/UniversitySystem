using Microsoft.EntityFrameworkCore;
using UniversitySystemSummer.Data.Entities;

namespace UniversitySystemSummer.Data
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversityDbContext).Assembly);
        }
    }
}