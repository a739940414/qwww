using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Domain.Entities;

public class Account : BaseEntity
{
    public int? ParentId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public AccountType Type { get; set; }
    public int CurrencyId { get; set; }
    public int? BranchId { get; set; }
    public int? CostCenterId { get; set; }
    public string? CustomFieldsJson { get; set; }

    public Account? Parent { get; set; }
    public ICollection<Account> Children { get; set; } = new HashSet<Account>();
}
