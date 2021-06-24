using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.Infra.Persistence.Configurations
{
    class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Name is required and just 250 characters
            builder.Property(b => b.Designation)
                .IsRequired()
                .HasMaxLength(250)
                .HasDefaultValue("NA");
        }
    }
}