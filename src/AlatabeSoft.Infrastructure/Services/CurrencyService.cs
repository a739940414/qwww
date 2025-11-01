using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Application.Interfaces;
using AlatabeSoft.Domain.Entities;
using AlatabeSoft.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AlatabeSoft.Infrastructure.Services;

public class CurrencyService : ICurrencyService
{
    private readonly AlatabeSoftDbContext _dbContext;

    public CurrencyService(AlatabeSoftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<CurrencyRateDto>> GetActiveRatesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Currencies
            .Where(x => x.IsActive)
            .Select(x => new CurrencyRateDto(x.Id, x.Code, x.ExchangeRate, x.LastUpdated))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateExchangeRateAsync(int currencyId, decimal rate, DateTime effectiveOn, string source, CancellationToken cancellationToken = default)
    {
        var currency = await _dbContext.Currencies
            .FirstOrDefaultAsync(x => x.Id == currencyId, cancellationToken)
            ?? throw new InvalidOperationException($"Currency with id {currencyId} was not found.");

        currency.ExchangeRate = rate;
        currency.LastUpdated = effectiveOn;

        _dbContext.ExchangeRateHistories.Add(new ExchangeRateHistory
        {
            CurrencyId = currencyId,
            Rate = rate,
            EffectiveOn = effectiveOn,
            Source = source
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
