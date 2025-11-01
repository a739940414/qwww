using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Domain.Entities;

namespace AlatabeSoft.Application.Services;

public static class JournalEntryMapper
{
    public static JournalEntry ToEntity(JournalEntryDto dto)
    {
        var entry = new JournalEntry
        {
            Id = dto.Id,
            Date = dto.Date,
            CurrencyId = dto.CurrencyId,
            ExchangeRate = dto.ExchangeRate,
            Description = dto.Description,
            Source = dto.Source,
            Details = dto.Details.Select(d => new JournalDetail
            {
                AccountId = d.AccountId,
                Debit = d.Debit,
                Credit = d.Credit,
                BranchId = d.BranchId,
                CostCenterId = d.CostCenterId,
                ProjectId = d.ProjectId,
                Notes = d.Notes
            }).ToList()
        };

        return entry;
    }

    public static JournalEntryDto ToDto(JournalEntry entity)
    {
        return new JournalEntryDto(
            entity.Id,
            entity.Date,
            entity.CurrencyId,
            entity.ExchangeRate,
            entity.Description,
            entity.Source,
            entity.Details.Select(d => new JournalDetailDto(
                d.AccountId,
                d.Debit,
                d.Credit,
                d.BranchId,
                d.CostCenterId,
                d.ProjectId,
                d.Notes)).ToList());
    }
}
