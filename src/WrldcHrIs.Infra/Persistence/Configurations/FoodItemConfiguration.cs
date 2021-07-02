using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.Infra.Persistence.Configurations
{
    public class FoodItemConfiguration : IEntityTypeConfiguration<FoodItem>
    {
        public void Configure(EntityTypeBuilder<FoodItem> builder)
        {
            // Name is required and just 250 characters
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.Description)
                .HasMaxLength(500);

            builder.Property(b => b.Price)
                .IsRequired()
                .HasDefaultValue(0);

            builder
            .HasIndex(b => b.Name)
            .IsUnique();

        }
    }
}