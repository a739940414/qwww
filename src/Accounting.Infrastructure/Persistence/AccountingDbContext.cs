using Accounting.Application.Abstractions;
using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Persistence;

public class AccountingDbContext : DbContext, IAccountingDbContext
{
    public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options)
    {
    }

    public DbSet<ChartOfAccount> ChartOfAccounts => Set<ChartOfAccount>();
    public DbSet<CostCenter> CostCenters => Set<CostCenter>();
    public DbSet<FiscalYear> FiscalYears => Set<FiscalYear>();
    public DbSet<FiscalPeriod> FiscalPeriods => Set<FiscalPeriod>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<JournalLine> JournalLines => Set<JournalLine>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<SalesInvoice> SalesInvoices => Set<SalesInvoice>();
    public DbSet<SalesInvoiceLine> SalesInvoiceLines => Set<SalesInvoiceLine>();
    public DbSet<Receipt> Receipts => Set<Receipt>();
    public DbSet<ReceiptApplication> ReceiptApplications => Set<ReceiptApplication>();
    public DbSet<PurchaseInvoice> PurchaseInvoices => Set<PurchaseInvoice>();
    public DbSet<PurchaseInvoiceLine> PurchaseInvoiceLines => Set<PurchaseInvoiceLine>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PaymentApplication> PaymentApplications => Set<PaymentApplication>();
    public DbSet<TaxCode> TaxCodes => Set<TaxCode>();
    public DbSet<TaxTransaction> TaxTransactions => Set<TaxTransaction>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountingDbContext).Assembly);
    }
}
