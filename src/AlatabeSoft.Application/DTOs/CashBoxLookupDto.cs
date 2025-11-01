namespace AlatabeSoft.Application.DTOs;

public record CashBoxLookupDto(
    int Id,
    string Name,
    int CurrencyId,
    string CurrencyCode,
    decimal ExchangeRate,
    int ControlAccountId,
    decimal Balance);
