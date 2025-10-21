using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.ToTable("ExchangeRates");
        builder.HasIndex(x => new { x.CurrencyId, x.RateDate }).IsUnique();
        builder.Property(x => x.BuyRate).HasColumnType("decimal(18,6)");
        builder.Property(x => x.SellRate).HasColumnType("decimal(18,6)");
    }
}
