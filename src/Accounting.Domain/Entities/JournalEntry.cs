using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities;

public class JournalEntry : BaseEntity
{
    public string JournalNumber { get; set; } = default!;
    public DateTime JournalDate { get; set; }
    public int FiscalPeriodId { get; set; }
    public FiscalPeriod? FiscalPeriod { get; set; }
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    public string? Description { get; set; }
    public bool IsPosted { get; set; }
    public string? PostedBy { get; set; }
    public DateTime? PostedOn { get; set; }
    public SourceModule SourceModule { get; set; }
    public string? ReferenceNumber { get; set; }
    public ICollection<JournalLine> Lines { get; set; } = new List<JournalLine>();
}
