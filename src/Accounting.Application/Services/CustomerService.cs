using Accounting.Application.Abstractions;
using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IAccountingDbContext _context;

    public CustomerService(IAccountingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var customers = await _context.Customers
            .AsNoTracking()
            .OrderBy(c => c.Code)
            .ToListAsync(cancellationToken);

        return customers.Select(MapToDto);
    }

    public async Task<CustomerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return customer is null ? null : MapToDto(customer);
    }

    public async Task<int> CreateAsync(CustomerDto dto, CancellationToken cancellationToken = default)
    {
        if (await _context.Customers.AnyAsync(c => c.Code == dto.Code, cancellationToken))
        {
            throw new InvalidOperationException($"Customer with code {dto.Code} already exists.");
        }

        var entity = new Customer
        {
            Code = dto.Code,
            Name = dto.Name,
            TaxNumber = dto.TaxNumber,
            CreditLimit = dto.CreditLimit,
            PaymentTerms = dto.PaymentTerms,
            IsActive = dto.IsActive
        };

        _context.Customers.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(int id, CustomerDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Customer {id} was not found.");
        }

        entity.Name = dto.Name;
        entity.TaxNumber = dto.TaxNumber;
        entity.CreditLimit = dto.CreditLimit;
        entity.PaymentTerms = dto.PaymentTerms;
        entity.IsActive = dto.IsActive;
        entity.ModifiedOn = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static CustomerDto MapToDto(Customer entity) => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        Name = entity.Name,
        TaxNumber = entity.TaxNumber,
        CreditLimit = entity.CreditLimit,
        PaymentTerms = entity.PaymentTerms,
        IsActive = entity.IsActive
    };
}
