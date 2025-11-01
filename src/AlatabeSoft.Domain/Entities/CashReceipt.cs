namespace AlatabeSoft.Domain.Entities;

public class CashReceipt : BaseEntity
{
    public DateTime Date { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public int CashBoxId { get; set; }
    public int CurrencyId { get; set; }
    public decimal Amount { get; set; }
    public decimal ExchangeRate { get; set; }
    public int? CustomerId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int? JournalEntryId { get; set; }
    public int CreatedByUserId { get; set; }

    public CashBox? CashBox { get; set; }
    public Currency? Currency { get; set; }
    public BusinessEntity? Customer { get; set; }
    public JournalEntry? JournalEntry { get; set; }
}
