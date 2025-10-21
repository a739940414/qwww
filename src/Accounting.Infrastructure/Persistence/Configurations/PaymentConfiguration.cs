using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasIndex(x => x.DocumentNumber).IsUnique();
        builder.Property(x => x.DocumentNumber).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Amount).HasColumnType("decimal(18,3)");
        builder.Property(x => x.Reference).HasMaxLength(100);
    }
}
