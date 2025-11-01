namespace AlatabeSoft.Domain.Entities;

public class Bank : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public int CurrencyId { get; set; }
    public decimal Balance { get; set; }
    public int BranchId { get; set; }

    public Currency? Currency { get; set; }
    public Branch? Branch { get; set; }
}
