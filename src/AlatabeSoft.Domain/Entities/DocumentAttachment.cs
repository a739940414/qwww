namespace AlatabeSoft.Domain.Entities;

public class DocumentAttachment : BaseEntity
{
    public string EntityName { get; set; } = string.Empty;
    public int EntityId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[] Data { get; set; } = Array.Empty<byte>();
}
