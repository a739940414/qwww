using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatabeSoft.Infrastructure.Persistence.Configurations;

public class DocumentAttachmentConfiguration : IEntityTypeConfiguration<DocumentAttachment>
{
    public void Configure(EntityTypeBuilder<DocumentAttachment> builder)
    {
        builder.ToTable("DocumentAttachments");
        builder.Property(x => x.EntityName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.FileName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.ContentType).HasMaxLength(100).IsRequired();
    }
}
