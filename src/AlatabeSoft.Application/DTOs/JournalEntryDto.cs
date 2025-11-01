using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Application.DTOs;

public record JournalEntryDto(
    int Id,
    DateTime Date,
    int CurrencyId,
    decimal ExchangeRate,
    string Description,
    JournalEntrySource Source,
    IReadOnlyCollection<JournalDetailDto> Details);

public record JournalDetailDto(
    int AccountId,
    decimal Debit,
    decimal Credit,
    int? BranchId,
    int? CostCenterId,
    int? ProjectId,
    string? Notes);
