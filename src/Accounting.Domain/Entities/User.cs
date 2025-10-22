namespace Accounting.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string PasswordSalt { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
}
