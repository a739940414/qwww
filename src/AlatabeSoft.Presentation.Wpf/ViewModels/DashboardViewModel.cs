using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlatabeSoft.Application.Interfaces;

namespace AlatabeSoft.Presentation.Wpf.ViewModels;

public class DashboardViewModel : ViewModelBase, IAsyncLoadable
{
    private readonly ICashManagementService _cashManagementService;
    private bool _isLoaded;

    public DashboardViewModel(ICashManagementService cashManagementService)
    {
        _cashManagementService = cashManagementService;
    }

    public DashboardSummaryViewModel Summary { get; } = new();
    public ObservableCollection<TimelineItemViewModel> RecentTransactions { get; } = new();
    public ObservableCollection<InventoryAlertItemViewModel> InventoryAlerts { get; } = new();

    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        if (_isLoaded)
        {
            return;
        }

        var cashBoxes = await _cashManagementService.GetCashBoxesAsync(cancellationToken);
        Summary.TotalBalance = cashBoxes.Sum(x => x.Balance);
        Summary.AvailableCash = Summary.TotalBalance;

        var receipts = await _cashManagementService.GetRecentCashReceiptsAsync(5, cancellationToken);
        var payments = await _cashManagementService.GetRecentCashPaymentsAsync(5, cancellationToken);

        var today = DateTime.Today;
        Summary.TodaySales = receipts.Where(x => x.Date.Date == today).Sum(x => x.Amount);
        Summary.TodayExpenses = payments.Where(x => x.Date.Date == today).Sum(x => x.Amount);

        RecentTransactions.Clear();

        var timeline = receipts.Select(r => new TimelineEntry(r.Date, "سند قبض", $"{r.CashBoxName} - {r.Amount:N2} {r.CurrencyCode}"))
            .Concat(payments.Select(p => new TimelineEntry(p.Date, "سند صرف", $"{p.ExpenseAccountName} - {p.Amount:N2} {p.CurrencyCode}")))
            .OrderByDescending(x => x.Date)
            .Take(10);

        foreach (var item in timeline)
        {
            RecentTransactions.Add(new TimelineItemViewModel(item.Title, $"{item.Date:yyyy/MM/dd} - {item.Details}"));
        }

        if (!InventoryAlerts.Any())
        {
            InventoryAlerts.Add(new InventoryAlertItemViewModel("حاسوب مكتبي", "الرصيد الحالي أقل من الحد الأدنى"));
            InventoryAlerts.Add(new InventoryAlertItemViewModel("مواد مكتبية", "اقترب موعد إعادة الطلب"));
        }

        _isLoaded = true;
    }
}

public class DashboardSummaryViewModel : ViewModelBase
{
    private decimal _totalBalance;
    private decimal _availableCash;
    private decimal _todaySales;
    private decimal _todayExpenses;

    public decimal TotalBalance
    {
        get => _totalBalance;
        set => SetProperty(ref _totalBalance, value);
    }

    public decimal AvailableCash
    {
        get => _availableCash;
        set => SetProperty(ref _availableCash, value);
    }

    public decimal TodaySales
    {
        get => _todaySales;
        set => SetProperty(ref _todaySales, value);
    }

    public decimal TodayExpenses
    {
        get => _todayExpenses;
        set => SetProperty(ref _todayExpenses, value);
    }
}

public record TimelineItemViewModel(string Title, string Details);

public record InventoryAlertItemViewModel(string ItemName, string Message);

internal record TimelineEntry(DateTime Date, string Title, string Details);
