namespace Accounting.Domain.Entities;

public class Supplier : BaseEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? TaxNumber { get; set; }
    public string? PaymentTerms { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<PurchaseInvoice> Invoices { get; set; } = new List<PurchaseInvoice>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
