namespace Accounting.Domain.Entities;

public class CostCenter : BaseEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int? ParentId { get; set; }
    public CostCenter? Parent { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<CostCenter> Children { get; set; } = new List<CostCenter>();
}
