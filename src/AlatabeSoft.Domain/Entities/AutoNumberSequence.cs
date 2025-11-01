namespace AlatabeSoft.Domain.Entities;

public class AutoNumberSequence : BaseEntity
{
    public string EntityName { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
    public int NextNumber { get; set; }
    public int Padding { get; set; } = 5;

    public string GenerateNext() => $"{Prefix}{NextNumber.ToString($"D{Padding}")}";
}
