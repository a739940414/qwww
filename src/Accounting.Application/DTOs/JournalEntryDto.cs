using Accounting.Domain.Enums;

namespace Accounting.Application.DTOs;

public class JournalEntryDto
{
    public int? Id { get; set; }
    public string JournalNumber { get; set; } = default!;
    public DateTime JournalDate { get; set; }
    public int FiscalPeriodId { get; set; }
    public int CurrencyId { get; set; }
    public string? Description { get; set; }
    public SourceModule SourceModule { get; set; }
    public string? ReferenceNumber { get; set; }
    public List<JournalLineDto> Lines { get; set; } = new();
}

public class JournalLineDto
{
    public int AccountId { get; set; }
    public int? CostCenterId { get; set; }
    public string? Description { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public int? CurrencyId { get; set; }
    public decimal? FxRate { get; set; }
}
