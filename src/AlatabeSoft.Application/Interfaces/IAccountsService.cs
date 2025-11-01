using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Application.Interfaces;

public interface IAccountsService
{
    Task<int> EnsureControlAccountAsync(
        string accountCode,
        string accountName,
        AccountType accountType = AccountType.Asset,
        CancellationToken cancellationToken = default);
    Task<JournalEntryDto> PostAutomaticEntryAsync(string source, string referenceNumber, IReadOnlyCollection<JournalDetailDto> details, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<AccountDto>> GetAccountsAsync(AccountType? type = null, CancellationToken cancellationToken = default);
    Task<AccountDto> CreateAsync(AccountDto account, CancellationToken cancellationToken = default);
}
