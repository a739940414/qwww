namespace AlatabeSoft.Application.DTOs;

public record CurrencyDto(
    int Id,
    string Code,
    string Name,
    string Symbol,
    bool IsBaseCurrency,
    decimal ExchangeRate,
    bool IsActive,
    DateTime LastUpdated);
