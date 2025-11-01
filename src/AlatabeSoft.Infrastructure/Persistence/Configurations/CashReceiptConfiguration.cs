using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class CashReceiptConfiguration : IEntityTypeConfiguration<CashReceipt>
{
    public void Configure(EntityTypeBuilder<CashReceipt> builder)
    {
        builder.ToTable("CashReceipts");
        builder.Property(x => x.Amount).HasPrecision(18, 6);
        builder.Property(x => x.ExchangeRate).HasPrecision(18, 6);
        builder.Property(x => x.Description).HasMaxLength(255);
    }
}
