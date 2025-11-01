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

    public async Task<int> EnsureControlAccountAsync(string accountCode, string accountName, CancellationToken cancellationToken = default)
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
            Type = AccountType.Asset,
            CurrencyId = await _dbContext.Currencies.Where(x => x.IsBaseCurrency).Select(x => x.Id).FirstAsync(cancellationToken)
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
}
