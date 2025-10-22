namespace Accounting.Domain.Entities;

public class BankAccount : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Branch { get; set; }
    public string AccountNumber { get; set; } = default!;
    public string? Iban { get; set; }
}
