namespace AlatabeSoft.Domain.Entities;

public class CostCenter : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    public ICollection<JournalDetail> JournalDetails { get; set; } = new HashSet<JournalDetail>();
}
