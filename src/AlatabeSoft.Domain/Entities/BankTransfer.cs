namespace AlatabeSoft.Domain.Entities;

public class BankTransfer : BaseEntity
{
    public DateTime Date { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public int CurrencyId { get; set; }
    public decimal ExchangeRate { get; set; }
    public string Description { get; set; } = string.Empty;
    public int? JournalEntryId { get; set; }

    public Bank? FromAccount { get; set; }
    public Bank? ToAccount { get; set; }
    public Currency? Currency { get; set; }
    public JournalEntry? JournalEntry { get; set; }
}
