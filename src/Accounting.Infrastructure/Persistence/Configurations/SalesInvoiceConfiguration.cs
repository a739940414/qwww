using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class SalesInvoiceConfiguration : IEntityTypeConfiguration<SalesInvoice>
{
    public void Configure(EntityTypeBuilder<SalesInvoice> builder)
    {
        builder.ToTable("SalesInvoices");
        builder.HasIndex(x => x.DocumentNumber).IsUnique();
        builder.Property(x => x.DocumentNumber).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.NetAmount).HasColumnType("decimal(18,3)");
        builder.Property(x => x.TaxAmount).HasColumnType("decimal(18,3)");
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,3)");
        builder.HasMany(x => x.Lines)
            .WithOne(x => x.SalesInvoice)
            .HasForeignKey(x => x.SalesInvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
