using Accounting.Application.DTOs;

namespace Accounting.Application.Interfaces;

public interface IJournalService
{
    Task<int> CreateAsync(JournalEntryDto dto, string userId, CancellationToken cancellationToken = default);
    Task PostAsync(int journalId, string userId, CancellationToken cancellationToken = default);
}
