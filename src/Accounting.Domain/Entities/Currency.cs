namespace Accounting.Domain.Entities;

public class Currency : BaseEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsBase { get; set; }
    public ICollection<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();
}
