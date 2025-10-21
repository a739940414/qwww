using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class ReceiptApplicationConfiguration : IEntityTypeConfiguration<ReceiptApplication>
{
    public void Configure(EntityTypeBuilder<ReceiptApplication> builder)
    {
        builder.ToTable("ReceiptApplications");
        builder.HasIndex(x => new { x.ReceiptId, x.SalesInvoiceId }).IsUnique();
        builder.Property(x => x.AmountApplied).HasColumnType("decimal(18,3)");
    }
}
