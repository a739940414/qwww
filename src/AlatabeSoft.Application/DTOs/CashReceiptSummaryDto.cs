namespace AlatabeSoft.Application.DTOs;

public record CashReceiptSummaryDto(
    int Id,
    string ReferenceNumber,
    DateTime Date,
    string CashBoxName,
    string CurrencyCode,
    decimal Amount,
    decimal ExchangeRate,
    string? CustomerName,
    string Description);
