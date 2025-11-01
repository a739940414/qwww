using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("InventoryItems");
        builder.Property(x => x.Code).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Unit).HasMaxLength(20).IsRequired();
        builder.Property(x => x.MinimumQuantity).HasPrecision(18, 6);
        builder.Property(x => x.Cost).HasPrecision(18, 6);
        builder.Property(x => x.SalePrice).HasPrecision(18, 6);
        builder.Property(x => x.CurrentQuantity).HasPrecision(18, 6);
    }
}
