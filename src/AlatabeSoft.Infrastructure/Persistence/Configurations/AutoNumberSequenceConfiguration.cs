using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class AutoNumberSequenceConfiguration : IEntityTypeConfiguration<AutoNumberSequence>
{
    public void Configure(EntityTypeBuilder<AutoNumberSequence> builder)
    {
        builder.ToTable("AutoNumberSequences");
        builder.Property(x => x.EntityName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Prefix).HasMaxLength(10);
    }
}
