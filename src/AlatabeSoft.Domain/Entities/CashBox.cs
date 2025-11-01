namespace AlatabeSoft.Domain.Entities;

public class CashBox : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int CurrencyId { get; set; }
    public decimal Balance { get; set; }
    public int BranchId { get; set; }
    public int? ResponsibleUserId { get; set; }

    public Currency? Currency { get; set; }
    public Branch? Branch { get; set; }
    public ICollection<CashReceipt> CashReceipts { get; set; } = new HashSet<CashReceipt>();
    public ICollection<CashPayment> CashPayments { get; set; } = new HashSet<CashPayment>();
}
