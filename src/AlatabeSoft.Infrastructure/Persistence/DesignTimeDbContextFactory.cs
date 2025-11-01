using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AlatabeSoft.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AlatabeSoftDbContext>
{
    public AlatabeSoftDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AlatabeSoftDbContext>();
        optionsBuilder.UseSqlite("Data Source=alatabesoft.db");

        return new AlatabeSoftDbContext(optionsBuilder.Options);
    }
}
