namespace AlatabeSoft.Domain.ValueObjects;

public readonly record struct Money(decimal Amount, int CurrencyId, decimal ExchangeRate)
{
    public decimal BaseAmount => Amount * ExchangeRate;
}
