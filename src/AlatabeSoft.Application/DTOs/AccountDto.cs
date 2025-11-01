using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Application.DTOs;

public record AccountDto(
    int Id,
    string Code,
    string Name,
    AccountType Type,
    int CurrencyId,
    int? ParentId,
    int? BranchId,
    int? CostCenterId,
    bool IsActive);
