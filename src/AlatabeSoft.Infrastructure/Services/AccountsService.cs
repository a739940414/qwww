using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Application.Interfaces;
using AlatabeSoft.Domain.Entities;
using AlatabeSoft.Domain.Enums;
using AlatabeSoft.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AlatabeSoft.Infrastructure.Services;

public class AccountsService : IAccountsService
{
    private readonly AlatabeSoftDbContext _dbContext;
    private readonly IJournalEntryService _journalEntryService;

    public AccountsService(AlatabeSoftDbContext dbContext, IJournalEntryService journalEntryService)
    {
        _dbContext = dbContext;
        _journalEntryService = journalEntryService;
    }

    public async Task<int> EnsureControlAccountAsync(
        string accountCode,
        string accountName,
        AccountType accountType = AccountType.Asset,
        CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Code == accountCode, cancellationToken);
        if (account is not null)
        {
            return account.Id;
        }

        account = new Account
        {
            Code = accountCode,
            Name = accountName,
            Type = accountType,
            CurrencyId = await _dbContext.Currencies.Where(x => x.IsBaseCurrency).Select(x => x.Id).FirstAsync(cancellationToken),
            IsActive = true
        };

        _dbContext.Accounts.Add(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return account.Id;
    }

    public async Task<JournalEntryDto> PostAutomaticEntryAsync(string source, string referenceNumber, IReadOnlyCollection<JournalDetailDto> details, CancellationToken cancellationToken = default)
    {
        var currencyId = await _dbContext.Currencies
            .Where(x => x.IsBaseCurrency)
            .Select(x => x.Id)
            .FirstAsync(cancellationToken);

        if (!Enum.TryParse<JournalEntrySource>(source, ignoreCase: true, out var parsedSource))
        {
            parsedSource = JournalEntrySource.Manual;
        }

        var entry = new JournalEntryDto(
            0,
            DateTime.UtcNow,
            currencyId,
            1m,
            referenceNumber,
            parsedSource,
            details);

        return await _journalEntryService.CreateAsync(entry, cancellationToken);
    }

    public async Task<IReadOnlyCollection<AccountDto>> GetAccountsAsync(AccountType? type = null, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Accounts.AsQueryable();

        if (type.HasValue)
        {
            query = query.Where(x => x.Type == type);
        }

        return await query
            .OrderBy(x => x.Code)
            .Select(x => new AccountDto(x.Id, x.Code, x.Name, x.Type, x.CurrencyId, x.ParentId, x.BranchId, x.CostCenterId, x.IsActive))
            .ToListAsync(cancellationToken);
    }

    public async Task<AccountDto> CreateAsync(AccountDto account, CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Accounts.AnyAsync(x => x.Code == account.Code, cancellationToken))
        {
            throw new InvalidOperationException($"An account with code {account.Code} already exists.");
        }

        var entity = new Account
        {
            Code = account.Code,
            Name = account.Name,
            Type = account.Type,
            CurrencyId = account.CurrencyId,
            ParentId = account.ParentId,
            BranchId = account.BranchId,
            CostCenterId = account.CostCenterId,
            IsActive = account.IsActive
        };

        _dbContext.Accounts.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AccountDto(entity.Id, entity.Code, entity.Name, entity.Type, entity.CurrencyId, entity.ParentId, entity.BranchId, entity.CostCenterId, entity.IsActive);
    }
}
