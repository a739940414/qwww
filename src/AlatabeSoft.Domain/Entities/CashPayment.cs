namespace AlatabeSoft.Domain.Entities;

public class CashPayment : BaseEntity
{
    public DateTime Date { get; set; }
    public int CashBoxId { get; set; }
    public int CurrencyId { get; set; }
    public decimal Amount { get; set; }
    public decimal ExchangeRate { get; set; }
    public int ExpenseAccountId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int? JournalEntryId { get; set; }
    public int CreatedByUserId { get; set; }

    public CashBox? CashBox { get; set; }
    public Currency? Currency { get; set; }
    public Account? ExpenseAccount { get; set; }
    public JournalEntry? JournalEntry { get; set; }
}
