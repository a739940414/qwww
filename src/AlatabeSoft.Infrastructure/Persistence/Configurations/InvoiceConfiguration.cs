using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");
        builder.Property(x => x.Number).HasMaxLength(30).IsRequired();
        builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(10).IsRequired();
        builder.Property(x => x.Total).HasPrecision(18, 6);
        builder.Property(x => x.Tax).HasPrecision(18, 6);
        builder.Property(x => x.Discount).HasPrecision(18, 6);

        builder.HasMany(x => x.Lines)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
