using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class BusinessEntityConfiguration : IEntityTypeConfiguration<BusinessEntity>
{
    public void Configure(EntityTypeBuilder<BusinessEntity> builder)
    {
        builder.ToTable("BusinessEntities");
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Phone).HasMaxLength(20);
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.CreditLimit).HasPrecision(18, 6);
        builder.Property(x => x.Balance).HasPrecision(18, 6);
    }
}
