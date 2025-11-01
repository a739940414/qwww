namespace AlatabeSoft.Application.DTOs;

public record CashPaymentSummaryDto(
    int Id,
    string ReferenceNumber,
    DateTime Date,
    string CashBoxName,
    string CurrencyCode,
    decimal Amount,
    decimal ExchangeRate,
    string ExpenseAccountName,
    string Description);
