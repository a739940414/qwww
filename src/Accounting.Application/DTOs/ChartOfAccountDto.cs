using Accounting.Domain.Enums;

namespace Accounting.Application.DTOs;

public class ChartOfAccountDto
{
    public int? Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Level { get; set; }
    public int? ParentId { get; set; }
    public AccountType AccountType { get; set; }
    public bool IsPostable { get; set; }
    public bool IsActive { get; set; }
}
