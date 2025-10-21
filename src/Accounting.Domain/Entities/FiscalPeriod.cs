namespace Accounting.Domain.Entities;

public class FiscalPeriod : BaseEntity
{
    public string Name { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsOpen { get; set; } = true;
    public int FiscalYearId { get; set; }
    public FiscalYear? FiscalYear { get; set; }
}
