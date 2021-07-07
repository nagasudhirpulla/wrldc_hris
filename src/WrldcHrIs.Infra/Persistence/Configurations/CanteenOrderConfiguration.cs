using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.Infra.Persistence.Configurations
{
    public class CanteenOrderConfiguration : IEntityTypeConfiguration<CanteenOrder>
    {
        public void Configure(EntityTypeBuilder<CanteenOrder> builder)
        {
            builder.Property(b => b.CustomerId)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.OrderDate)
                .IsRequired();

            builder.Property(b => b.OrderQuantity)
                .IsRequired();

            builder.Property(b => b.FoodItemName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.FoodItemDescription)
                .HasMaxLength(500);

            builder.Property(b => b.FoodItemUnitPrice)
                .IsRequired();

            builder
                .HasIndex(b => new { b.OrderDate, b.CustomerId, b.FoodItemName })
                .IsUnique();

        }
    }
}