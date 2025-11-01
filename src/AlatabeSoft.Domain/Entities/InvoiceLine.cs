namespace AlatabeSoft.Domain.Entities;

public class InvoiceLine : BaseEntity
{
    public int InvoiceId { get; set; }
    public int ItemId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }

    public Invoice? Invoice { get; set; }
    public InventoryItem? Item { get; set; }
}
