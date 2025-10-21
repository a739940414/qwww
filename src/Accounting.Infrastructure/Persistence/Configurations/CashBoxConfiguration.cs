using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class CashBoxConfiguration : IEntityTypeConfiguration<CashBox>
{
    public void Configure(EntityTypeBuilder<CashBox> builder)
    {
        builder.ToTable("CashBoxes");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Location).HasMaxLength(150);
    }
}
