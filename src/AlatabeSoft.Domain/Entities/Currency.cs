namespace AlatabeSoft.Domain.Entities;

public class Currency : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
    public bool IsBaseCurrency { get; set; }
    public DateTime LastUpdated { get; set; }

    public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
}
