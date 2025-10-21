using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities;

public class TaxTransaction : BaseEntity
{
    public SourceModule SourceModule { get; set; }
    public int SourceId { get; set; }
    public int TaxCodeId { get; set; }
    public TaxCode? TaxCode { get; set; }
    public decimal BaseAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public DateTime DocumentDate { get; set; }
}
