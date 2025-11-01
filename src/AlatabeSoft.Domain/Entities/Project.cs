namespace AlatabeSoft.Domain.Entities;

public class Project : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }

    public ICollection<JournalDetail> JournalDetails { get; set; } = new HashSet<JournalDetail>();
}
