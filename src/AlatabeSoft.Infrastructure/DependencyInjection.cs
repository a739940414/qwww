using AlatabeSoft.Application.Interfaces;
using AlatabeSoft.Infrastructure.Persistence;
using AlatabeSoft.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlatabeSoft.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? "Data Source=alatabesoft.db";

        services.AddDbContext<AlatabeSoftDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IJournalEntryService, JournalEntryService>();
        services.AddScoped<IAccountsService, AccountsService>();
        services.AddScoped<IBusinessEntityService, BusinessEntityService>();
        services.AddScoped<ICashManagementService, CashManagementService>();
        services.AddTransient<DatabaseSeeder>();

        return services;
    }
}
