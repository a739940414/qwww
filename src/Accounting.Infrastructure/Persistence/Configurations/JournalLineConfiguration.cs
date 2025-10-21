using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class JournalLineConfiguration : IEntityTypeConfiguration<JournalLine>
{
    public void Configure(EntityTypeBuilder<JournalLine> builder)
    {
        builder.ToTable("JournalLines");
        builder.Property(x => x.Debit).HasColumnType("decimal(18,3)");
        builder.Property(x => x.Credit).HasColumnType("decimal(18,3)");
        builder.Property(x => x.Description).HasMaxLength(300);
        builder.HasCheckConstraint("CK_JournalLines_DebitCredit", "NOT (Debit > 0 AND Credit > 0)");
    }
}
