using Accounting.Application.DTOs;

namespace Accounting.Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CustomerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(CustomerDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CustomerDto dto, CancellationToken cancellationToken = default);
}
