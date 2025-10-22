namespace Accounting.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = default!;
    public ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();
    public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
}
