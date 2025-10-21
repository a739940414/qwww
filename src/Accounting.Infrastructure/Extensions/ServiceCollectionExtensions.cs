using Accounting.Application.Abstractions;
using Accounting.Application.Interfaces;
using Accounting.Application.Services;
using Accounting.Infrastructure.Persistence;
using Accounting.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Accounting.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccountingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AccountingConnection")
            ?? throw new InvalidOperationException("Connection string 'AccountingConnection' was not found.");

        services.AddDbContext<AccountingDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IAccountingDbContext>(provider => provider.GetRequiredService<AccountingDbContext>());

        services.AddScoped<IChartOfAccountService, ChartOfAccountService>();
        services.AddScoped<IJournalService, JournalService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ISupplierService, SupplierService>();

        return services;
    }

    public static async Task EnsureDatabaseMigratedAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();
        var hasMigrations = (await context.Database.GetMigrationsAsync(cancellationToken)).Any();

        if (hasMigrations)
        {
            await context.Database.MigrateAsync(cancellationToken);
        }
        else
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }
        await AccountingDbContextSeed.SeedAsync(context);
    }
}
