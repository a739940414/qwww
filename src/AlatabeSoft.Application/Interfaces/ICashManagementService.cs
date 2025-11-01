using AlatabeSoft.Application.DTOs;

namespace AlatabeSoft.Application.Interfaces;

public interface ICashManagementService
{
    Task<IReadOnlyCollection<CashBoxLookupDto>> GetCashBoxesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<BusinessEntityLookupDto>> GetCustomersAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CashReceiptSummaryDto>> GetRecentCashReceiptsAsync(int take, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CashPaymentSummaryDto>> GetRecentCashPaymentsAsync(int take, CancellationToken cancellationToken = default);
    Task<CashReceiptSummaryDto> CreateCashReceiptAsync(CashReceiptCreateDto request, CancellationToken cancellationToken = default);
    Task<CashPaymentSummaryDto> CreateCashPaymentAsync(CashPaymentCreateDto request, CancellationToken cancellationToken = default);
}
