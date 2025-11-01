using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class ExchangeRateHistoryConfiguration : IEntityTypeConfiguration<ExchangeRateHistory>
{
    public void Configure(EntityTypeBuilder<ExchangeRateHistory> builder)
    {
        builder.ToTable("ExchangeRateHistory");
        builder.Property(x => x.Rate).HasPrecision(18, 6);
        builder.Property(x => x.Source).HasMaxLength(100).IsRequired();
    }
}
