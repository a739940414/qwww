namespace AlatabeSoft.Domain.Entities;

public class InventoryAdjustment : BaseEntity
{
    public DateTime Date { get; set; }
    public int ItemId { get; set; }
    public decimal QuantityDifference { get; set; }
    public decimal CostImpact { get; set; }
    public string Reason { get; set; } = string.Empty;
    public int? JournalEntryId { get; set; }

    public InventoryItem? Item { get; set; }
    public JournalEntry? JournalEntry { get; set; }
}
