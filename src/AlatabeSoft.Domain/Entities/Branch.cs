namespace AlatabeSoft.Domain.Entities;

public class Branch : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }

    public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    public ICollection<CashBox> CashBoxes { get; set; } = new HashSet<CashBox>();
    public ICollection<Bank> Banks { get; set; } = new HashSet<Bank>();
}
