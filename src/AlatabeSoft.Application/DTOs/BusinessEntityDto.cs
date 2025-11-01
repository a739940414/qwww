using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Application.DTOs;

public record BusinessEntityDto(
    int Id,
    BusinessEntityType Type,
    string Name,
    string? Phone,
    string? Email,
    string? Address,
    int CurrencyId,
    string CurrencyCode,
    decimal CreditLimit,
    decimal Balance,
    int ControlAccountId);
