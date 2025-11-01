using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class BankConfiguration : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
        builder.ToTable("Banks");
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.AccountNumber).HasMaxLength(30).IsRequired();
        builder.Property(x => x.Balance).HasPrecision(18, 6);
    }
}
