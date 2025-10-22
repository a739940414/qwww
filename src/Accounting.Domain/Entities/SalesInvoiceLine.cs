namespace Accounting.Domain.Entities;

public class SalesInvoiceLine : BaseEntity
{
    public int SalesInvoiceId { get; set; }
    public SalesInvoice? SalesInvoice { get; set; }
    public string ItemCode { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineNet { get; set; }
    public decimal LineTax { get; set; }
    public decimal LineTotal { get; set; }
    public int RevenueAccountId { get; set; }
    public ChartOfAccount? RevenueAccount { get; set; }
    public int? TaxCodeId { get; set; }
    public TaxCode? TaxCode { get; set; }
}
