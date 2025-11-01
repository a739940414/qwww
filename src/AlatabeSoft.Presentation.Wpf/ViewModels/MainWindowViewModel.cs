using System.Collections.ObjectModel;

namespace AlatabeSoft.Presentation.Wpf.ViewModels;

public class MainWindowViewModel
{
    public ObservableCollection<NavigationItem> NavigationItems { get; } = new(
    [
        new NavigationItem("🏠", "الرئيسية"),
        new NavigationItem("💼", "الحسابات"),
        new NavigationItem("💰", "الصناديق"),
        new NavigationItem("🏦", "البنوك"),
        new NavigationItem("🧾", "القيود"),
        new NavigationItem("📊", "التقارير"),
        new NavigationItem("⚙️", "الإعدادات")
    ]);

    public DashboardSnapshot Dashboard { get; } = new()
    {
        TotalBalance = 152345.45m,
        AvailableCash = 76543.20m,
        TodaySales = 12999.99m,
        TodayExpenses = 4500.50m
    };

    public ObservableCollection<TimelineItem> RecentTransactions { get; } = new(
    [
        new TimelineItem("فاتورة بيع", "تم إنشاء فاتورة بيع رقم INV-2024-001"),
        new TimelineItem("سند قبض", "تحصيل دفعة من عميل شركة المستقبل"),
        new TimelineItem("تحويل بنكي", "تحويل من البنك الرئيسي إلى صندوق الفرع"),
        new TimelineItem("جرد", "تسوية مخزون لصنف أجهزة الشبكات")
    ]);

    public ObservableCollection<InventoryAlertItem> InventoryAlerts { get; } = new(
    [
        new InventoryAlertItem("حاسوب مكتبي", "الرصيد الحالي أقل من الحد الأدنى"),
        new InventoryAlertItem("حاسوب محمول", "تاريخ الشراء أقدم من 90 يوم")
    ]);

    public string SelectedBranch { get; set; } = "الفرع الرئيسي";
    public string CurrentUser { get; set; } = "محمد الأحمد";
}

public record NavigationItem(string Icon, string Title);

public class DashboardSnapshot
{
    public decimal TotalBalance { get; set; }
    public decimal AvailableCash { get; set; }
    public decimal TodaySales { get; set; }
    public decimal TodayExpenses { get; set; }
}

public record TimelineItem(string Title, string Details);

public record InventoryAlertItem(string ItemName, string Message);
