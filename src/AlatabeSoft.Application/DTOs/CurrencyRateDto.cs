namespace AlatabeSoft.Application.DTOs;

public record CurrencyRateDto(int CurrencyId, string Code, decimal ExchangeRate, DateTime LastUpdated);
