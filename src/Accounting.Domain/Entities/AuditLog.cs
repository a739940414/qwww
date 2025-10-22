namespace Accounting.Domain.Entities;

public class AuditLog : BaseEntity
{
    public string EntityType { get; set; } = default!;
    public int EntityId { get; set; }
    public string Action { get; set; } = default!;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string PerformedBy { get; set; } = default!;
    public DateTime PerformedOn { get; set; }
}
