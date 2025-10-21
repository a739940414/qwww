using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Persistence.Configurations;

public class FiscalPeriodConfiguration : IEntityTypeConfiguration<FiscalPeriod>
{
    public void Configure(EntityTypeBuilder<FiscalPeriod> builder)
    {
        builder.ToTable("FiscalPeriods");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.HasOne(x => x.FiscalYear)
            .WithMany(x => x.Periods)
            .HasForeignKey(x => x.FiscalYearId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
