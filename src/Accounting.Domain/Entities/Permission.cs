namespace Accounting.Domain.Entities;

public class Permission : BaseEntity
{
    public string Code { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ICollection<RolePermission> Roles { get; set; } = new List<RolePermission>();
}
