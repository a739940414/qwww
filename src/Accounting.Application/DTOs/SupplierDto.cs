namespace Accounting.Application.DTOs;

public class SupplierDto
{
    public int? Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? TaxNumber { get; set; }
    public string? PaymentTerms { get; set; }
    public bool IsActive { get; set; } = true;
}
