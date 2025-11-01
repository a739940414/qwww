using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class JournalDetailConfiguration : IEntityTypeConfiguration<JournalDetail>
{
    public void Configure(EntityTypeBuilder<JournalDetail> builder)
    {
        builder.ToTable("JournalDetails");
        builder.Property(x => x.Debit).HasPrecision(18, 6);
        builder.Property(x => x.Credit).HasPrecision(18, 6);
        builder.Property(x => x.Notes).HasMaxLength(255);
    }
}
