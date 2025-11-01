namespace AlatabeSoft.Application.DTOs;

public record CashPaymentCreateDto(
    string ReferenceNumber,
    DateTime Date,
    int CashBoxId,
    int CurrencyId,
    decimal Amount,
    decimal ExchangeRate,
    int ExpenseAccountId,
    string Description,
    int CreatedByUserId);
