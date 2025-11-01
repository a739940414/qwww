using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Application.Interfaces;
using AlatabeSoft.Application.Services;
using AlatabeSoft.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AlatabeSoft.Infrastructure.Services;

public class JournalEntryService : IJournalEntryService
{
    private readonly AlatabeSoftDbContext _dbContext;

    public JournalEntryService(AlatabeSoftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JournalEntryDto> CreateAsync(JournalEntryDto entry, CancellationToken cancellationToken = default)
    {
        var entity = JournalEntryMapper.ToEntity(entry);
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await _dbContext.JournalEntries.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        var persisted = await _dbContext.JournalEntries
            .Include(x => x.Details)
            .FirstAsync(x => x.Id == entity.Id, cancellationToken);

        return JournalEntryMapper.ToDto(persisted);
    }

    public async Task<IReadOnlyCollection<JournalEntryDto>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        var entries = await _dbContext.JournalEntries
            .Include(x => x.Details)
            .Where(x => x.Date >= from && x.Date <= to)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return entries.Select(JournalEntryMapper.ToDto).ToList();
    }
}
