using Accounting.Domain.Entities;
using Accounting.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Persistence.Seed;

public static class AccountingDbContextSeed
{
    public static async Task SeedAsync(AccountingDbContext context)
    {
        if (!await context.Currencies.AnyAsync())
        {
            context.Currencies.Add(new Currency { Code = "SAR", Name = "Saudi Riyal", IsBase = true });
            context.Currencies.Add(new Currency { Code = "USD", Name = "US Dollar", IsBase = false });
        }

        if (!await context.ChartOfAccounts.AnyAsync())
        {
            var assets = new ChartOfAccount { Code = "1000", Name = "Assets", Level = 1, AccountType = AccountType.Asset, IsPostable = false };
            var liabilities = new ChartOfAccount { Code = "2000", Name = "Liabilities", Level = 1, AccountType = AccountType.Liability, IsPostable = false };
            var equity = new ChartOfAccount { Code = "3000", Name = "Equity", Level = 1, AccountType = AccountType.Equity, IsPostable = false };
            var revenue = new ChartOfAccount { Code = "4000", Name = "Revenue", Level = 1, AccountType = AccountType.Revenue, IsPostable = false };
            var expenses = new ChartOfAccount { Code = "5000", Name = "Expenses", Level = 1, AccountType = AccountType.Expense, IsPostable = false };

            context.ChartOfAccounts.AddRange(assets, liabilities, equity, revenue, expenses);
            context.ChartOfAccounts.Add(new ChartOfAccount { Code = "1010", Name = "Cash on Hand", Level = 2, Parent = assets, AccountType = AccountType.Asset, IsPostable = true });
            context.ChartOfAccounts.Add(new ChartOfAccount { Code = "1100", Name = "Accounts Receivable", Level = 2, Parent = assets, AccountType = AccountType.Asset, IsPostable = true });
            context.ChartOfAccounts.Add(new ChartOfAccount { Code = "2100", Name = "Accounts Payable", Level = 2, Parent = liabilities, AccountType = AccountType.Liability, IsPostable = true });
            context.ChartOfAccounts.Add(new ChartOfAccount { Code = "4100", Name = "Sales Revenue", Level = 2, Parent = revenue, AccountType = AccountType.Revenue, IsPostable = true });
            context.ChartOfAccounts.Add(new ChartOfAccount { Code = "5100", Name = "Cost of Goods Sold", Level = 2, Parent = expenses, AccountType = AccountType.Expense, IsPostable = true });
        }

        if (!await context.CostCenters.AnyAsync())
        {
            context.CostCenters.Add(new CostCenter { Code = "CC-ADMIN", Name = "Administration" });
            context.CostCenters.Add(new CostCenter { Code = "CC-SALES", Name = "Sales" });
        }

        if (!await context.FiscalYears.AnyAsync())
        {
            var currentYear = new FiscalYear
            {
                Name = DateTime.UtcNow.Year.ToString(),
                StartDate = new DateTime(DateTime.UtcNow.Year, 1, 1),
                EndDate = new DateTime(DateTime.UtcNow.Year, 12, 31),
                IsClosed = false,
                Periods = Enumerable.Range(1, 12).Select(m => new FiscalPeriod
                {
                    Name = $"{DateTime.UtcNow.Year}-{m:00}",
                    StartDate = new DateTime(DateTime.UtcNow.Year, m, 1),
                    EndDate = new DateTime(DateTime.UtcNow.Year, m, DateTime.DaysInMonth(DateTime.UtcNow.Year, m)),
                    IsOpen = true
                }).ToList()
            };
            context.FiscalYears.Add(currentYear);
        }

        if (!await context.TaxCodes.AnyAsync())
        {
            var vatAccount = await context.ChartOfAccounts.FirstAsync(x => x.Code == "2100");
            context.TaxCodes.Add(new TaxCode { Code = "VAT15", Description = "VAT 15%", Rate = 0.15m, AccountId = vatAccount.Id });
        }

        await context.SaveChangesAsync();
    }
}
