namespace Accounting.Domain.Entities;

public class ReceiptApplication : BaseEntity
{
    public int ReceiptId { get; set; }
    public Receipt? Receipt { get; set; }
    public int SalesInvoiceId { get; set; }
    public SalesInvoice? SalesInvoice { get; set; }
    public decimal AmountApplied { get; set; }
}
