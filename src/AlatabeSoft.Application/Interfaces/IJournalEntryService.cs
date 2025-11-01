using AlatabeSoft.Application.DTOs;

namespace AlatabeSoft.Application.Interfaces;

public interface IJournalEntryService
{
    Task<JournalEntryDto> CreateAsync(JournalEntryDto entry, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<JournalEntryDto>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
}
