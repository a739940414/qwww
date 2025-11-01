using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Domain.Entities;

public class BusinessEntity : BaseEntity
{
    public BusinessEntityType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public int CurrencyId { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Balance { get; set; }
    public int ControlAccountId { get; set; }

    public Currency? Currency { get; set; }
    public Account? ControlAccount { get; set; }
    public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    public ICollection<CashReceipt> CashReceipts { get; set; } = new HashSet<CashReceipt>();
}
