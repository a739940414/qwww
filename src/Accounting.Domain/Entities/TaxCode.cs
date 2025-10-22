namespace Accounting.Domain.Entities;

public class TaxCode : BaseEntity
{
    public string Code { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Rate { get; set; }
    public int AccountId { get; set; }
    public ChartOfAccount? Account { get; set; }
}
