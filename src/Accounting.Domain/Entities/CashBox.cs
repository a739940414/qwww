namespace Accounting.Domain.Entities;

public class CashBox : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Location { get; set; }
}
