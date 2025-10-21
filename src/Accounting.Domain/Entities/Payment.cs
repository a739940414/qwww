using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities;

public class Payment : BaseEntity
{
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public string DocumentNumber { get; set; } = default!;
    public DateTime DocumentDate { get; set; }
    public PaymentMethod Method { get; set; }
    public decimal Amount { get; set; }
    public int? BankAccountId { get; set; }
    public BankAccount? BankAccount { get; set; }
    public int? CashBoxId { get; set; }
    public CashBox? CashBox { get; set; }
    public string? Reference { get; set; }
    public ICollection<PurchaseInvoice> Invoices { get; set; } = new List<PurchaseInvoice>();
}
