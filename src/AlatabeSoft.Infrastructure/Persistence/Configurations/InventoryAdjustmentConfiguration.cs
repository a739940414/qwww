using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class InventoryAdjustmentConfiguration : IEntityTypeConfiguration<InventoryAdjustment>
{
    public void Configure(EntityTypeBuilder<InventoryAdjustment> builder)
    {
        builder.ToTable("InventoryAdjustments");
        builder.Property(x => x.QuantityDifference).HasPrecision(18, 6);
        builder.Property(x => x.CostImpact).HasPrecision(18, 6);
        builder.Property(x => x.Reason).HasMaxLength(255);
    }
}
