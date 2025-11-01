using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class CashBoxConfiguration : IEntityTypeConfiguration<CashBox>
{
    public void Configure(EntityTypeBuilder<CashBox> builder)
    {
        builder.ToTable("CashBoxes");
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Balance).HasPrecision(18, 6);
    }
}
