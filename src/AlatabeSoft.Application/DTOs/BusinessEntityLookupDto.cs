using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Application.DTOs;

public record BusinessEntityLookupDto(
    int Id,
    string Name,
    BusinessEntityType Type,
    int CurrencyId,
    string CurrencyCode,
    int AccountId,
    decimal Balance);
