namespace Accounting.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public byte[]? RowVersion { get; set; }
}
