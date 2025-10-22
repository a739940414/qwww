namespace Accounting.Domain.Entities;

public class Attachment : BaseEntity
{
    public string EntityType { get; set; } = default!;
    public int EntityId { get; set; }
    public string FileName { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public string StoragePath { get; set; } = default!;
}
