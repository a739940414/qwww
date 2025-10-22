using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities;

public class PurchaseInvoice : BaseEntity
{
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public string DocumentNumber { get; set; } = default!;
    public DateTime DocumentDate { get; set; }
    public DateTime? DueDate { get; set; }
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    public decimal NetAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public DocumentStatus Status { get; set; }
    public ICollection<PurchaseInvoiceLine> Lines { get; set; } = new List<PurchaseInvoiceLine>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
