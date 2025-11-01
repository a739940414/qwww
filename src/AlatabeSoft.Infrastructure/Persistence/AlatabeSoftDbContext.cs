using AlatabeSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlatabeSoft.Infrastructure.Persistence;

public class AlatabeSoftDbContext : DbContext
{
    public AlatabeSoftDbContext(DbContextOptions<AlatabeSoftDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<CashBox> CashBoxes => Set<CashBox>();
    public DbSet<Bank> Banks => Set<Bank>();
    public DbSet<CashReceipt> CashReceipts => Set<CashReceipt>();
    public DbSet<CashPayment> CashPayments => Set<CashPayment>();
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<JournalDetail> JournalDetails => Set<JournalDetail>();
    public DbSet<BusinessEntity> BusinessEntities => Set<BusinessEntity>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<InventoryAdjustment> InventoryAdjustments => Set<InventoryAdjustment>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<CostCenter> CostCenters => Set<CostCenter>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<BankTransfer> BankTransfers => Set<BankTransfer>();
    public DbSet<ExchangeRateHistory> ExchangeRateHistories => Set<ExchangeRateHistory>();
    public DbSet<DocumentAttachment> DocumentAttachments => Set<DocumentAttachment>();
    public DbSet<AutoNumberSequence> AutoNumberSequences => Set<AutoNumberSequence>();
    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlatabeSoftDbContext).Assembly);
    }
}
