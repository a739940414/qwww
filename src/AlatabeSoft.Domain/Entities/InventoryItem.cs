namespace AlatabeSoft.Domain.Entities;

public class InventoryItem : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal MinimumQuantity { get; set; }
    public decimal Cost { get; set; }
    public decimal SalePrice { get; set; }
    public decimal CurrentQuantity { get; set; }

    public ICollection<InvoiceLine> InvoiceLines { get; set; } = new HashSet<InvoiceLine>();
}
