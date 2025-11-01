using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Application.DTOs;

public record BusinessEntityCreateDto(
    BusinessEntityType Type,
    string Name,
    string? Phone,
    string? Email,
    string? Address,
    int CurrencyId,
    decimal CreditLimit,
    int? AccountId);
