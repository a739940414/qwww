using System.Collections.Generic;
using System.Linq;
using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Application.Interfaces;
using AlatabeSoft.Domain.Entities;
using AlatabeSoft.Domain.Enums;
using AlatabeSoft.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AlatabeSoft.Infrastructure.Services;

public class CashManagementService : ICashManagementService
{
    private readonly AlatabeSoftDbContext _dbContext;
    private readonly IAccountsService _accountsService;

    public CashManagementService(AlatabeSoftDbContext dbContext, IAccountsService accountsService)
    {
        _dbContext = dbContext;
        _accountsService = accountsService;
    }

    public async Task<IReadOnlyCollection<CashBoxLookupDto>> GetCashBoxesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.CashBoxes
            .Include(x => x.Currency)
            .OrderBy(x => x.Name)
            .Select(x => new CashBoxLookupDto(x.Id, x.Name, x.CurrencyId, x.Currency!.Code, x.Currency.ExchangeRate, x.ControlAccountId, x.Balance))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<BusinessEntityLookupDto>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.BusinessEntities
            .Include(x => x.Currency)
            .Where(x => x.Type == BusinessEntityType.Customer)
            .OrderBy(x => x.Name)
            .Select(x => new BusinessEntityLookupDto(x.Id, x.Name, x.Type, x.CurrencyId, x.Currency!.Code, x.ControlAccountId, x.Balance))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<CashReceiptSummaryDto>> GetRecentCashReceiptsAsync(int take, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CashReceipts
            .Include(x => x.CashBox).ThenInclude(b => b!.Currency)
            .Include(x => x.Customer)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.Id)
            .Take(take)
            .Select(x => new CashReceiptSummaryDto(x.Id, x.ReferenceNumber, x.Date, x.CashBox!.Name, x.CashBox.Currency!.Code, x.Amount, x.ExchangeRate, x.Customer != null ? x.Customer.Name : null, x.Description))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<CashPaymentSummaryDto>> GetRecentCashPaymentsAsync(int take, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CashPayments
            .Include(x => x.CashBox).ThenInclude(b => b!.Currency)
            .Include(x => x.ExpenseAccount)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.Id)
            .Take(take)
            .Select(x => new CashPaymentSummaryDto(x.Id, x.ReferenceNumber, x.Date, x.CashBox!.Name, x.CashBox.Currency!.Code, x.Amount, x.ExchangeRate, x.ExpenseAccount!.Name, x.Description))
            .ToListAsync(cancellationToken);
    }

    public async Task<CashReceiptSummaryDto> CreateCashReceiptAsync(CashReceiptCreateDto request, CancellationToken cancellationToken = default)
    {
        var cashBox = await _dbContext.CashBoxes.Include(x => x.Currency).FirstAsync(x => x.Id == request.CashBoxId, cancellationToken);
        var customer = request.CustomerId.HasValue
            ? await _dbContext.BusinessEntities.Include(x => x.ControlAccount).FirstAsync(x => x.Id == request.CustomerId.Value, cancellationToken)
            : null;

        var entity = new CashReceipt
        {
            ReferenceNumber = string.IsNullOrWhiteSpace(request.ReferenceNumber) ? GenerateReference("RCPT") : request.ReferenceNumber.Trim(),
            Date = request.Date,
            CashBoxId = request.CashBoxId,
            CurrencyId = request.CurrencyId,
            Amount = request.Amount,
            ExchangeRate = request.ExchangeRate,
            CustomerId = request.CustomerId,
            Description = request.Description,
            CreatedByUserId = request.CreatedByUserId
        };

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            _dbContext.CashReceipts.Add(entity);
            cashBox.Balance += request.Amount;

            if (customer is not null)
            {
                customer.Balance -= request.Amount;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            var details = new List<JournalDetailDto>
            {
                new(cashBox.ControlAccountId, request.Amount, 0m, null, null, null, $"سند قبض {entity.ReferenceNumber}"),
            };

            if (customer is not null)
            {
                details.Add(new(customer.ControlAccountId, 0m, request.Amount, null, null, null, $"سند قبض {entity.ReferenceNumber}"));
            }

            var journal = await _accountsService.PostAutomaticEntryAsync(nameof(CashReceipt), entity.ReferenceNumber, details, cancellationToken);
            entity.JournalEntryId = journal.Id;

            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        var refreshed = await _dbContext.CashReceipts
            .Include(x => x.CashBox).ThenInclude(b => b!.Currency)
            .Include(x => x.Customer)
            .FirstAsync(x => x.Id == entity.Id, cancellationToken);

        return new CashReceiptSummaryDto(
            refreshed.Id,
            refreshed.ReferenceNumber,
            refreshed.Date,
            refreshed.CashBox!.Name,
            refreshed.CashBox.Currency!.Code,
            refreshed.Amount,
            refreshed.ExchangeRate,
            refreshed.Customer?.Name,
            refreshed.Description);
    }

    public async Task<CashPaymentSummaryDto> CreateCashPaymentAsync(CashPaymentCreateDto request, CancellationToken cancellationToken = default)
    {
        var cashBox = await _dbContext.CashBoxes.Include(x => x.Currency).FirstAsync(x => x.Id == request.CashBoxId, cancellationToken);
        var expenseAccount = await _dbContext.Accounts.FirstAsync(x => x.Id == request.ExpenseAccountId, cancellationToken);

        var entity = new CashPayment
        {
            ReferenceNumber = string.IsNullOrWhiteSpace(request.ReferenceNumber) ? GenerateReference("PMT") : request.ReferenceNumber.Trim(),
            Date = request.Date,
            CashBoxId = request.CashBoxId,
            CurrencyId = request.CurrencyId,
            Amount = request.Amount,
            ExchangeRate = request.ExchangeRate,
            ExpenseAccountId = request.ExpenseAccountId,
            Description = request.Description,
            CreatedByUserId = request.CreatedByUserId
        };

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            _dbContext.CashPayments.Add(entity);
            cashBox.Balance -= request.Amount;

            await _dbContext.SaveChangesAsync(cancellationToken);

            var details = new List<JournalDetailDto>
            {
                new(expenseAccount.Id, request.Amount, 0m, null, null, null, $"سند صرف {entity.ReferenceNumber}"),
                new(cashBox.ControlAccountId, 0m, request.Amount, null, null, null, $"سند صرف {entity.ReferenceNumber}")
            };

            var journal = await _accountsService.PostAutomaticEntryAsync(nameof(CashPayment), entity.ReferenceNumber, details, cancellationToken);
            entity.JournalEntryId = journal.Id;

            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        var refreshed = await _dbContext.CashPayments
            .Include(x => x.CashBox).ThenInclude(b => b!.Currency)
            .Include(x => x.ExpenseAccount)
            .FirstAsync(x => x.Id == entity.Id, cancellationToken);

        return new CashPaymentSummaryDto(
            refreshed.Id,
            refreshed.ReferenceNumber,
            refreshed.Date,
            refreshed.CashBox!.Name,
            refreshed.CashBox.Currency!.Code,
            refreshed.Amount,
            refreshed.ExchangeRate,
            refreshed.ExpenseAccount!.Name,
            refreshed.Description);
    }

    private static string GenerateReference(string prefix)
    {
        return $"{prefix}-{DateTime.UtcNow:yyyyMMddHHmmss}";
    }
}
