namespace Accounting.Domain.Entities;

public class Customer : BaseEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? TaxNumber { get; set; }
    public decimal? CreditLimit { get; set; }
    public string? PaymentTerms { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<SalesInvoice> Invoices { get; set; } = new List<SalesInvoice>();
    public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
