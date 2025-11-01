using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Domain.Enums;

namespace AlatabeSoft.Application.Interfaces;

public interface IBusinessEntityService
{
    Task<IReadOnlyCollection<BusinessEntityDto>> GetAsync(
        BusinessEntityType? type = null,
        CancellationToken cancellationToken = default);

    Task<BusinessEntityDto> CreateAsync(
        BusinessEntityCreateDto request,
        CancellationToken cancellationToken = default);
}
