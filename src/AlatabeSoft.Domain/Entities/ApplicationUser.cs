namespace AlatabeSoft.Domain.Entities;

public class ApplicationUser : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool CanApprove { get; set; }
    public int? BranchId { get; set; }

    public Branch? Branch { get; set; }
}
