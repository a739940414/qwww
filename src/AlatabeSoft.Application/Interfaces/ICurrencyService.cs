using AlatabeSoft.Application.DTOs;

namespace AlatabeSoft.Application.Interfaces;

public interface ICurrencyService
{
    Task<IReadOnlyCollection<CurrencyRateDto>> GetActiveRatesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CurrencyDto>> GetCurrenciesAsync(CancellationToken cancellationToken = default);
    Task UpdateExchangeRateAsync(int currencyId, decimal rate, DateTime effectiveOn, string source, CancellationToken cancellationToken = default);
}
