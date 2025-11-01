namespace AlatabeSoft.Domain.Entities;

public class AuditLog : BaseEntity
{
    public DateTime ActionDate { get; set; }
    public int UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityKey { get; set; } = string.Empty;
    public string? Changes { get; set; }

    public ApplicationUser? User { get; set; }
}
