using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasIndex(x => x.Username).IsUnique();
        builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(500);
        builder.Property(x => x.PasswordSalt).IsRequired().HasMaxLength(500);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Email).HasMaxLength(200);
    }
}
