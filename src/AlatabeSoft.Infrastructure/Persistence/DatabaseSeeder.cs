using System.Collections.Generic;
using System.Linq;
using AlatabeSoft.Domain.Entities;
using AlatabeSoft.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlatabeSoft.Infrastructure.Persistence;

public class DatabaseSeeder
{
    private readonly AlatabeSoftDbContext _dbContext;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(AlatabeSoftDbContext dbContext, ILogger<DatabaseSeeder> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting database seed");
        await _dbContext.Database.EnsureCreatedAsync(cancellationToken);

        await SeedCurrenciesAsync(cancellationToken);
        await SeedBranchesAsync(cancellationToken);
        await SeedAccountsAsync(cancellationToken);
        await SeedCashBoxesAsync(cancellationToken);
        await SeedBusinessEntitiesAsync(cancellationToken);

        _logger.LogInformation("Database seed completed");
    }

    private async Task SeedCurrenciesAsync(CancellationToken cancellationToken)
    {
        if (await _dbContext.Currencies.AnyAsync(cancellationToken))
        {
            return;
        }

        var now = DateTime.UtcNow;

        var sar = new Currency
        {
            Code = "SAR",
            Name = "ريال سعودي",
            Symbol = "﷼",
            ExchangeRate = 1m,
            IsBaseCurrency = true,
            IsActive = true,
            LastUpdated = now
        };

        var usd = new Currency
        {
            Code = "USD",
            Name = "دولار أمريكي",
            Symbol = "$",
            ExchangeRate = 3.75m,
            IsBaseCurrency = false,
            IsActive = true,
            LastUpdated = now
        };

        await _dbContext.Currencies.AddRangeAsync(sar, usd, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedBranchesAsync(CancellationToken cancellationToken)
    {
        if (await _dbContext.Branches.AnyAsync(cancellationToken))
        {
            return;
        }

        await _dbContext.Branches.AddAsync(new Branch
        {
            Name = "الفرع الرئيسي",
            Code = "MAIN"
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedAccountsAsync(CancellationToken cancellationToken)
    {
        if (await _dbContext.Accounts.AnyAsync(cancellationToken))
        {
            return;
        }

        var baseCurrencyId = await _dbContext.Currencies
            .Where(x => x.IsBaseCurrency)
            .Select(x => x.Id)
            .FirstAsync(cancellationToken);

        var accounts = new List<Account>
        {
            new()
            {
                Code = "1000",
                Name = "الأصول المتداولة",
                Type = AccountType.Asset,
                CurrencyId = baseCurrencyId
            },
            new()
            {
                Code = "1100",
                Name = "الصندوق الرئيسي",
                Type = AccountType.Asset,
                CurrencyId = baseCurrencyId,
                ParentId = null
            },
            new()
            {
                Code = "1200",
                Name = "العملاء",
                Type = AccountType.Asset,
                CurrencyId = baseCurrencyId,
                ParentId = null
            },
            new()
            {
                Code = "2100",
                Name = "الموردون",
                Type = AccountType.Liability,
                CurrencyId = baseCurrencyId,
                ParentId = null
            },
            new()
            {
                Code = "5100",
                Name = "مصروفات عامة",
                Type = AccountType.Expense,
                CurrencyId = baseCurrencyId
            }
        };

        await _dbContext.Accounts.AddRangeAsync(accounts, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedCashBoxesAsync(CancellationToken cancellationToken)
    {
        if (await _dbContext.CashBoxes.AnyAsync(cancellationToken))
        {
            return;
        }

        var baseCurrency = await _dbContext.Currencies.FirstAsync(x => x.IsBaseCurrency, cancellationToken);
        var cashAccount = await _dbContext.Accounts.FirstAsync(x => x.Code == "1100", cancellationToken);
        var branch = await _dbContext.Branches.FirstAsync(cancellationToken);

        await _dbContext.CashBoxes.AddAsync(new CashBox
        {
            Name = "صندوق الفرع الرئيسي",
            CurrencyId = baseCurrency.Id,
            AccountId = cashAccount.Id,
            Balance = 0m,
            BranchId = branch.Id
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedBusinessEntitiesAsync(CancellationToken cancellationToken)
    {
        if (await _dbContext.BusinessEntities.AnyAsync(cancellationToken))
        {
            return;
        }

        var baseCurrency = await _dbContext.Currencies.FirstAsync(x => x.IsBaseCurrency, cancellationToken);
        var receivableAccount = await _dbContext.Accounts.FirstAsync(x => x.Code == "1200", cancellationToken);
        var payableAccount = await _dbContext.Accounts.FirstAsync(x => x.Code == "2100", cancellationToken);

        await _dbContext.BusinessEntities.AddRangeAsync(new BusinessEntity
        {
            Type = BusinessEntityType.Customer,
            Name = "شركة المستقبل",
            Phone = "0500000000",
            Email = "customer@example.com",
            Address = "الرياض",
            CurrencyId = baseCurrency.Id,
            CreditLimit = 50000m,
            Balance = 0m,
            AccountId = receivableAccount.Id
        },
        new BusinessEntity
        {
            Type = BusinessEntityType.Supplier,
            Name = "مؤسسة التوريد",
            Phone = "0112345678",
            Email = "supplier@example.com",
            Address = "جدة",
            CurrencyId = baseCurrency.Id,
            CreditLimit = 0m,
            Balance = 0m,
            AccountId = payableAccount.Id
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
