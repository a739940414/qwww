using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Domain.Entities;

public class Invoice : BaseEntity
{
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public InvoiceType Type { get; set; }
    public int? CustomerId { get; set; }
    public int? SupplierId { get; set; }
    public int CurrencyId { get; set; }
    public decimal ExchangeRate { get; set; }
    public decimal Total { get; set; }
    public decimal Tax { get; set; }
    public decimal Discount { get; set; }
    public InvoiceStatus Status { get; set; }
    public int? JournalEntryId { get; set; }

    public BusinessEntity? Customer { get; set; }
    public BusinessEntity? Supplier { get; set; }
    public Currency? Currency { get; set; }
    public JournalEntry? JournalEntry { get; set; }
    public ICollection<InvoiceLine> Lines { get; set; } = new HashSet<InvoiceLine>();
}
