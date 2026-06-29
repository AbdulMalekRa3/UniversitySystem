using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversitySystemSummer.Data.Entities;

namespace UniversitySystemSummer.Data.Configurations
{
    public class UserClaimMapping : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("UserClaims");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("UserClaimId");
        }
    }
}
