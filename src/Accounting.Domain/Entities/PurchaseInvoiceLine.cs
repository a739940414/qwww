namespace Accounting.Domain.Entities;

public class PurchaseInvoiceLine : BaseEntity
{
    public int PurchaseInvoiceId { get; set; }
    public PurchaseInvoice? PurchaseInvoice { get; set; }
    public string ItemCode { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineNet { get; set; }
    public decimal LineTax { get; set; }
    public decimal LineTotal { get; set; }
    public int ExpenseAccountId { get; set; }
    public ChartOfAccount? ExpenseAccount { get; set; }
    public int? TaxCodeId { get; set; }
    public TaxCode? TaxCode { get; set; }
}
