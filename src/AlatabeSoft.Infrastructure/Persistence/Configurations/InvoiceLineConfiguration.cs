using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.ToTable("InvoiceLines");
        builder.Property(x => x.Quantity).HasPrecision(18, 6);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 6);
        builder.Property(x => x.Discount).HasPrecision(18, 6);
        builder.Property(x => x.Tax).HasPrecision(18, 6);
    }
}
