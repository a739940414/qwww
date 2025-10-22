namespace Accounting.Domain.Entities;

public class ExchangeRate : BaseEntity
{
    public DateTime RateDate { get; set; }
    public decimal BuyRate { get; set; }
    public decimal SellRate { get; set; }
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
}
