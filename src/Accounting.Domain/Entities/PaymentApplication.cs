namespace Accounting.Domain.Entities;

public class PaymentApplication : BaseEntity
{
    public int PaymentId { get; set; }
    public Payment? Payment { get; set; }
    public int PurchaseInvoiceId { get; set; }
    public PurchaseInvoice? PurchaseInvoice { get; set; }
    public decimal AmountApplied { get; set; }
}
