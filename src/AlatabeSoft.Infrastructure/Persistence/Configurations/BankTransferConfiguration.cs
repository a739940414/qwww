using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class BankTransferConfiguration : IEntityTypeConfiguration<BankTransfer>
{
    public void Configure(EntityTypeBuilder<BankTransfer> builder)
    {
        builder.ToTable("BankTransfers");
        builder.Property(x => x.Amount).HasPrecision(18, 6);
        builder.Property(x => x.ExchangeRate).HasPrecision(18, 6);
        builder.Property(x => x.Description).HasMaxLength(255);

        builder.HasOne(x => x.FromAccount)
            .WithMany()
            .HasForeignKey(x => x.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ToAccount)
            .WithMany()
            .HasForeignKey(x => x.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
