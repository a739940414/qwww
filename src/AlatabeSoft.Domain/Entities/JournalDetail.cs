namespace AlatabeSoft.Domain.Entities;

public class JournalDetail : BaseEntity
{
    public int JournalEntryId { get; set; }
    public int AccountId { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public int? BranchId { get; set; }
    public int? CostCenterId { get; set; }
    public int? ProjectId { get; set; }
    public string? Notes { get; set; }

    public JournalEntry? JournalEntry { get; set; }
    public Account? Account { get; set; }
    public Branch? Branch { get; set; }
    public CostCenter? CostCenter { get; set; }
    public Project? Project { get; set; }
}
