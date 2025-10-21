using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class PaymentApplicationConfiguration : IEntityTypeConfiguration<PaymentApplication>
{
    public void Configure(EntityTypeBuilder<PaymentApplication> builder)
    {
        builder.ToTable("PaymentApplications");
        builder.HasIndex(x => new { x.PaymentId, x.PurchaseInvoiceId }).IsUnique();
        builder.Property(x => x.AmountApplied).HasColumnType("decimal(18,3)");
    }
}
