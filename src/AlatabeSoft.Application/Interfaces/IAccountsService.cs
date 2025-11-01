using AlatabeSoft.Application.DTOs;

namespace AlatabeSoft.Application.Interfaces;

public interface IAccountsService
{
    Task<int> EnsureControlAccountAsync(string accountCode, string accountName, CancellationToken cancellationToken = default);
    Task<JournalEntryDto> PostAutomaticEntryAsync(string source, string referenceNumber, IReadOnlyCollection<JournalDetailDto> details, CancellationToken cancellationToken = default);
}
