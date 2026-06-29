using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversitySystemSummer.Data.Entities;

namespace UniversitySystemSummer.Data.Configurations
{
    public class RoleClaimMapping : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaims");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("RoleClaimId");
        }
    }

}
