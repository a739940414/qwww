using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Domain.Entities;

public class JournalEntry : BaseEntity
{
    public DateTime Date { get; set; }
    public int CurrencyId { get; set; }
    public decimal ExchangeRate { get; set; }
    public string Description { get; set; } = string.Empty;
    public JournalEntrySource Source { get; set; }
    public string? ReferenceNumber { get; set; }

    public Currency? Currency { get; set; }
    public ICollection<JournalDetail> Details { get; set; } = new HashSet<JournalDetail>();
}
