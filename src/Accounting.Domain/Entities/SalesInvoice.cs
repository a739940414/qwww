using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities;

public class SalesInvoice : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public string DocumentNumber { get; set; } = default!;
    public DateTime DocumentDate { get; set; }
    public DateTime? DueDate { get; set; }
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    public decimal NetAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public DocumentStatus Status { get; set; }
    public ICollection<SalesInvoiceLine> Lines { get; set; } = new List<SalesInvoiceLine>();
    public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
