using Accounting.Application.Abstractions;
using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Accounting.Domain.Entities;
using Accounting.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Application.Services;

public class JournalService : IJournalService
{
    private readonly IAccountingDbContext _context;

    public JournalService(IAccountingDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(JournalEntryDto dto, string userId, CancellationToken cancellationToken = default)
    {
        if (dto.Lines.Count == 0)
        {
            throw new InvalidOperationException("Journal entry must contain at least one line.");
        }

        if (!dto.Lines.Any(l => l.Debit > 0) || !dto.Lines.Any(l => l.Credit > 0))
        {
            throw new InvalidOperationException("Journal entry must have both debit and credit lines.");
        }

        var debitTotal = Math.Round(dto.Lines.Sum(l => l.Debit), 3);
        var creditTotal = Math.Round(dto.Lines.Sum(l => l.Credit), 3);
        if (debitTotal != creditTotal)
        {
            throw new InvalidOperationException("Journal entry is not balanced.");
        }

        var period = await _context.FiscalPeriods.FirstOrDefaultAsync(x => x.Id == dto.FiscalPeriodId, cancellationToken);
        if (period is null)
        {
            throw new InvalidOperationException("Fiscal period was not found.");
        }

        if (!period.IsOpen)
        {
            throw new InvalidOperationException("Fiscal period is closed or frozen.");
        }

        var accounts = await _context.ChartOfAccounts
            .Where(a => dto.Lines.Select(l => l.AccountId).Contains(a.Id))
            .ToDictionaryAsync(a => a.Id, cancellationToken);

        foreach (var line in dto.Lines)
        {
            if (!accounts.TryGetValue(line.AccountId, out var account) || !account.IsActive || !account.IsPostable)
            {
                throw new InvalidOperationException($"Account {line.AccountId} is not active or postable.");
            }
        }

        var entity = new JournalEntry
        {
            JournalNumber = dto.JournalNumber,
            JournalDate = dto.JournalDate,
            FiscalPeriodId = dto.FiscalPeriodId,
            CurrencyId = dto.CurrencyId,
            Description = dto.Description,
            SourceModule = dto.SourceModule,
            ReferenceNumber = dto.ReferenceNumber,
            CreatedBy = userId,
            Lines = dto.Lines.Select(l => new JournalLine
            {
                AccountId = l.AccountId,
                CostCenterId = l.CostCenterId,
                Description = l.Description,
                Debit = l.Debit,
                Credit = l.Credit,
                CurrencyId = l.CurrencyId,
                FxRate = l.FxRate
            }).ToList()
        };

        _context.JournalEntries.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        await LogAuditAsync("JournalEntry", entity.Id, "Create", null, dto, userId, cancellationToken);

        return entity.Id;
    }

    public async Task PostAsync(int journalId, string userId, CancellationToken cancellationToken = default)
    {
        var journal = await _context.JournalEntries
            .Include(j => j.Lines)
            .FirstOrDefaultAsync(j => j.Id == journalId, cancellationToken);

        if (journal is null)
        {
            throw new KeyNotFoundException("Journal entry not found.");
        }

        if (journal.IsPosted)
        {
            return;
        }

        var debitTotal = Math.Round(journal.Lines.Sum(l => l.Debit), 3);
        var creditTotal = Math.Round(journal.Lines.Sum(l => l.Credit), 3);
        if (debitTotal != creditTotal)
        {
            throw new InvalidOperationException("Journal entry is not balanced.");
        }

        var period = await _context.FiscalPeriods.FirstAsync(p => p.Id == journal.FiscalPeriodId, cancellationToken);
        if (!period.IsOpen)
        {
            throw new InvalidOperationException("Fiscal period is closed or frozen.");
        }

        journal.IsPosted = true;
        journal.PostedBy = userId;
        journal.PostedOn = DateTime.UtcNow;
        journal.ModifiedOn = DateTime.UtcNow;
        journal.ModifiedBy = userId;

        await _context.SaveChangesAsync(cancellationToken);

        await LogAuditAsync("JournalEntry", journal.Id, "Post", null, new { journal.Id, journal.PostedBy, journal.PostedOn }, userId, cancellationToken);
    }

    private async Task LogAuditAsync(string entityType, int entityId, string action, object? oldValue, object? newValue, string userId, CancellationToken cancellationToken)
    {
        var log = new AuditLog
        {
            EntityType = entityType,
            EntityId = entityId,
            Action = action,
            OldValue = oldValue is null ? null : System.Text.Json.JsonSerializer.Serialize(oldValue),
            NewValue = newValue is null ? null : System.Text.Json.JsonSerializer.Serialize(newValue),
            PerformedBy = userId,
            PerformedOn = DateTime.UtcNow
        };

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
