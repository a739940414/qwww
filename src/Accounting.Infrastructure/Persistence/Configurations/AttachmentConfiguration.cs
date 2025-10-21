using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments");
        builder.Property(x => x.EntityType).IsRequired().HasMaxLength(100);
        builder.Property(x => x.FileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.ContentType).IsRequired().HasMaxLength(100);
        builder.Property(x => x.StoragePath).IsRequired().HasMaxLength(500);
    }
}
