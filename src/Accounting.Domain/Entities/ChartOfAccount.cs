using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities;

public class ChartOfAccount : BaseEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Level { get; set; }
    public int? ParentId { get; set; }
    public ChartOfAccount? Parent { get; set; }
    public AccountType AccountType { get; set; }
    public bool IsPostable { get; set; } = true;
    public bool IsActive { get; set; } = true;
    public ICollection<ChartOfAccount> Children { get; set; } = new List<ChartOfAccount>();
}
