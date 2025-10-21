using Accounting.Application.DTOs;

namespace Accounting.Application.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SupplierDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(SupplierDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, SupplierDto dto, CancellationToken cancellationToken = default);
}
