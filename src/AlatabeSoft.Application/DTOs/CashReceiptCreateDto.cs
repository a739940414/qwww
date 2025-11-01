namespace AlatabeSoft.Application.DTOs;

public record CashReceiptCreateDto(
    string ReferenceNumber,
    DateTime Date,
    int CashBoxId,
    int CurrencyId,
    decimal Amount,
    decimal ExchangeRate,
    int? CustomerId,
    string Description,
    int CreatedByUserId);
