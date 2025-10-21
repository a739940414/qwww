using Accounting.Application.Abstractions;
using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly IAccountingDbContext _context;

    public SupplierService(IAccountingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SupplierDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var suppliers = await _context.Suppliers
            .AsNoTracking()
            .OrderBy(c => c.Code)
            .ToListAsync(cancellationToken);

        return suppliers.Select(MapToDto);
    }

    public async Task<SupplierDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var supplier = await _context.Suppliers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return supplier is null ? null : MapToDto(supplier);
    }

    public async Task<int> CreateAsync(SupplierDto dto, CancellationToken cancellationToken = default)
    {
        if (await _context.Suppliers.AnyAsync(c => c.Code == dto.Code, cancellationToken))
        {
            throw new InvalidOperationException($"Supplier with code {dto.Code} already exists.");
        }

        var entity = new Supplier
        {
            Code = dto.Code,
            Name = dto.Name,
            TaxNumber = dto.TaxNumber,
            PaymentTerms = dto.PaymentTerms,
            IsActive = dto.IsActive
        };

        _context.Suppliers.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(int id, SupplierDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Supplier {id} was not found.");
        }

        entity.Name = dto.Name;
        entity.TaxNumber = dto.TaxNumber;
        entity.PaymentTerms = dto.PaymentTerms;
        entity.IsActive = dto.IsActive;
        entity.ModifiedOn = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static SupplierDto MapToDto(Supplier entity) => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        Name = entity.Name,
        TaxNumber = entity.TaxNumber,
        PaymentTerms = entity.PaymentTerms,
        IsActive = entity.IsActive
    };
}
