namespace AlatabeSoft.Domain.Entities;

public class ExchangeRateHistory : BaseEntity
{
    public int CurrencyId { get; set; }
    public decimal Rate { get; set; }
    public DateTime EffectiveOn { get; set; }
    public string Source { get; set; } = string.Empty;

    public Currency? Currency { get; set; }
}
