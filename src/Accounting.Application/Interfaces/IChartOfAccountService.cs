using Accounting.Application.DTOs;

namespace Accounting.Application.Interfaces;

public interface IChartOfAccountService
{
    Task<IEnumerable<ChartOfAccountDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ChartOfAccountDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(ChartOfAccountDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, ChartOfAccountDto dto, CancellationToken cancellationToken = default);
    Task DeactivateAsync(int id, CancellationToken cancellationToken = default);
}
