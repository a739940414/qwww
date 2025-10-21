using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class TaxTransactionConfiguration : IEntityTypeConfiguration<TaxTransaction>
{
    public void Configure(EntityTypeBuilder<TaxTransaction> builder)
    {
        builder.ToTable("TaxTransactions");
        builder.Property(x => x.BaseAmount).HasColumnType("decimal(18,3)");
        builder.Property(x => x.TaxAmount).HasColumnType("decimal(18,3)");
    }
}
