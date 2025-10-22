using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class PurchaseInvoiceConfiguration : IEntityTypeConfiguration<PurchaseInvoice>
{
    public void Configure(EntityTypeBuilder<PurchaseInvoice> builder)
    {
        builder.ToTable("PurchaseInvoices");
        builder.HasIndex(x => x.DocumentNumber).IsUnique();
        builder.Property(x => x.DocumentNumber).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.NetAmount).HasColumnType("decimal(18,3)");
        builder.Property(x => x.TaxAmount).HasColumnType("decimal(18,3)");
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,3)");
    }
}
