using System;
using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Application.Interfaces;
using AlatabeSoft.Domain.Entities;
using AlatabeSoft.Domain.Enums;
using AlatabeSoft.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AlatabeSoft.Infrastructure.Services;

public class BusinessEntityService : IBusinessEntityService
{
    private readonly AlatabeSoftDbContext _dbContext;
    private readonly IAccountsService _accountsService;

    public BusinessEntityService(AlatabeSoftDbContext dbContext, IAccountsService accountsService)
    {
        _dbContext = dbContext;
        _accountsService = accountsService;
    }

    public async Task<IReadOnlyCollection<BusinessEntityDto>> GetAsync(
        BusinessEntityType? type = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.BusinessEntities
            .Include(x => x.Currency)
            .AsQueryable();

        if (type.HasValue)
        {
            query = query.Where(x => x.Type == type.Value);
        }

        return await query
            .OrderBy(x => x.Name)
            .Select(x => new BusinessEntityDto(
                x.Id,
                x.Type,
                x.Name,
                x.Phone,
                x.Email,
                x.Address,
                x.CurrencyId,
                x.Currency!.Code,
                x.CreditLimit,
                x.Balance,
                x.AccountId))
            .ToListAsync(cancellationToken);
    }

    public async Task<BusinessEntityDto> CreateAsync(
        BusinessEntityCreateDto request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required", nameof(request));
        }

        var trimmedName = request.Name.Trim();

        var exists = await _dbContext.BusinessEntities.AnyAsync(
            x => x.Name == trimmedName && x.Type == request.Type,
            cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException($"A {request.Type} with the same name already exists.");
        }

        var accountId = request.AccountId ?? await EnsureDefaultAccountAsync(request.Type, cancellationToken);

        var entity = new BusinessEntity
        {
            Type = request.Type,
            Name = trimmedName,
            Phone = request.Phone?.Trim(),
            Email = request.Email?.Trim(),
            Address = request.Address?.Trim(),
            CurrencyId = request.CurrencyId,
            CreditLimit = request.CreditLimit,
            Balance = 0m,
            AccountId = accountId
        };

        _dbContext.BusinessEntities.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var currency = await _dbContext.Currencies
            .Where(x => x.Id == entity.CurrencyId)
            .Select(x => new { x.Id, x.Code })
            .FirstAsync(cancellationToken);

        return new BusinessEntityDto(
            entity.Id,
            entity.Type,
            entity.Name,
            entity.Phone,
            entity.Email,
            entity.Address,
            currency.Id,
            currency.Code,
            entity.CreditLimit,
            entity.Balance,
            entity.AccountId);
    }

    private async Task<int> EnsureDefaultAccountAsync(BusinessEntityType type, CancellationToken cancellationToken)
    {
        return type switch
        {
            BusinessEntityType.Customer => await _accountsService.EnsureControlAccountAsync(
                "1200",
                "العملاء",
                AccountType.Asset,
                cancellationToken),
            BusinessEntityType.Supplier => await _accountsService.EnsureControlAccountAsync(
                "2100",
                "الموردين",
                AccountType.Liability,
                cancellationToken),
            _ => await _accountsService.EnsureControlAccountAsync(
                "1200",
                "العملاء",
                AccountType.Asset,
                cancellationToken)
        };
    }
}
