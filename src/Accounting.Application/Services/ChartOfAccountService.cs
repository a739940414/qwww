using Accounting.Application.Abstractions;
using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Application.Services;

public class ChartOfAccountService : IChartOfAccountService
{
    private readonly IAccountingDbContext _context;

    public ChartOfAccountService(IAccountingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ChartOfAccountDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var accounts = await _context.ChartOfAccounts
            .AsNoTracking()
            .OrderBy(a => a.Code)
            .ToListAsync(cancellationToken);

        return accounts.Select(MapToDto);
    }

    public async Task<ChartOfAccountDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.ChartOfAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity is null ? null : MapToDto(entity);
    }

    public async Task<int> CreateAsync(ChartOfAccountDto dto, CancellationToken cancellationToken = default)
    {
        if (await _context.ChartOfAccounts.AnyAsync(x => x.Code == dto.Code, cancellationToken))
        {
            throw new InvalidOperationException($"Account with code {dto.Code} already exists.");
        }

        var entity = new ChartOfAccount
        {
            Code = dto.Code,
            Name = dto.Name,
            Level = dto.Level,
            ParentId = dto.ParentId,
            AccountType = dto.AccountType,
            IsPostable = dto.IsPostable,
            IsActive = dto.IsActive
        };

        _context.ChartOfAccounts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(int id, ChartOfAccountDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _context.ChartOfAccounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Account {id} was not found.");
        }

        entity.Name = dto.Name;
        entity.Level = dto.Level;
        entity.ParentId = dto.ParentId;
        entity.AccountType = dto.AccountType;
        entity.IsPostable = dto.IsPostable;
        entity.IsActive = dto.IsActive;
        entity.ModifiedOn = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeactivateAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.ChartOfAccounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Account {id} was not found.");
        }

        entity.IsActive = false;
        entity.ModifiedOn = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static ChartOfAccountDto MapToDto(ChartOfAccount entity) => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        Name = entity.Name,
        Level = entity.Level,
        ParentId = entity.ParentId,
        AccountType = entity.AccountType,
        IsPostable = entity.IsPostable,
        IsActive = entity.IsActive
    };
}
