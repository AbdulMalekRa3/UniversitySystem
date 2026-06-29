using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversitySystemSummer.Data.Entities;

namespace UniversitySystemSummer.Data
{
    public class UniversityDbContext : IdentityDbContext< User, Role,
      int,
      UserClaim,
      UserRole,
      UserLogin,
      RoleClaim,
      UserToken>
    {
        public DbSet<Student> Students { get; set; }
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversityDbContext).Assembly);
        }
    }
}