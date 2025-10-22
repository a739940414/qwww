using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Application.Abstractions;

public interface IAccountingDbContext
{
    DbSet<ChartOfAccount> ChartOfAccounts { get; }
    DbSet<CostCenter> CostCenters { get; }
    DbSet<FiscalYear> FiscalYears { get; }
    DbSet<FiscalPeriod> FiscalPeriods { get; }
    DbSet<Currency> Currencies { get; }
    DbSet<ExchangeRate> ExchangeRates { get; }
    DbSet<JournalEntry> JournalEntries { get; }
    DbSet<JournalLine> JournalLines { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<SalesInvoice> SalesInvoices { get; }
    DbSet<SalesInvoiceLine> SalesInvoiceLines { get; }
    DbSet<Receipt> Receipts { get; }
    DbSet<ReceiptApplication> ReceiptApplications { get; }
    DbSet<PurchaseInvoice> PurchaseInvoices { get; }
    DbSet<PurchaseInvoiceLine> PurchaseInvoiceLines { get; }
    DbSet<Payment> Payments { get; }
    DbSet<PaymentApplication> PaymentApplications { get; }
    DbSet<TaxCode> TaxCodes { get; }
    DbSet<TaxTransaction> TaxTransactions { get; }
    DbSet<BankAccount> BankAccounts { get; }
    DbSet<CashBox> CashBoxes { get; }
    DbSet<Attachment> Attachments { get; }
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<AuditLog> AuditLogs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
