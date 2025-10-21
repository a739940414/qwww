using System.Windows;

namespace AccountingSystem.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnAccountsRibbonClick(object sender, RoutedEventArgs e) => ShowPlaceholder("الحسابات من الشريط العلوي");
    private void OnMaterialsRibbonClick(object sender, RoutedEventArgs e) => ShowPlaceholder("المواد من الشريط العلوي");
    private void OnSalesRibbonClick(object sender, RoutedEventArgs e) => ShowPlaceholder("المبيعات من الشريط العلوي");
    private void OnPurchasesRibbonClick(object sender, RoutedEventArgs e) => ShowPlaceholder("المشتريات من الشريط العلوي");
    private void OnManufacturingRibbonClick(object sender, RoutedEventArgs e) => ShowPlaceholder("التصنيع من الشريط العلوي");
    private void OnReportsRibbonClick(object sender, RoutedEventArgs e) => ShowPlaceholder("التقارير من الشريط العلوي");
    private void OnJournalRibbonClick(object sender, RoutedEventArgs e) => ShowPlaceholder("دفاتر اليومية من الشريط العلوي");
    private void OnToolsRibbonClick(object sender, RoutedEventArgs e) => ShowPlaceholder("الأدوات من الشريط العلوي");
    private void OnSettingsRibbonClick(object sender, RoutedEventArgs e) => ShowPlaceholder("الإعدادات من الشريط العلوي");

    private void OnDashboardClick(object sender, RoutedEventArgs e) => ShowPlaceholder("لوحة التحكم");
    private void OnAccountsClick(object sender, RoutedEventArgs e) => ShowPlaceholder("إدارة الحسابات");
    private void OnMaterialsClick(object sender, RoutedEventArgs e) => ShowPlaceholder("إدارة المواد");
    private void OnSalesClick(object sender, RoutedEventArgs e) => ShowPlaceholder("إدارة المبيعات");
    private void OnPurchasesClick(object sender, RoutedEventArgs e) => ShowPlaceholder("إدارة المشتريات");
    private void OnReportsClick(object sender, RoutedEventArgs e) => ShowPlaceholder("تقارير الأعمال");
    private void OnBackupClick(object sender, RoutedEventArgs e) => ShowPlaceholder("نسخ احتياطي");
    private void OnSettingsClick(object sender, RoutedEventArgs e) => ShowPlaceholder("إعدادات النظام");

    private void OnSalesTileClick(object sender, RoutedEventArgs e) => ShowPlaceholder("مؤشرات المبيعات");
    private void OnPurchasesTileClick(object sender, RoutedEventArgs e) => ShowPlaceholder("مؤشرات المشتريات");
    private void OnChartOfAccountsTileClick(object sender, RoutedEventArgs e) => ShowPlaceholder("لوحة دليل الحسابات");
    private void OnMaterialsTileClick(object sender, RoutedEventArgs e) => ShowPlaceholder("لوحة المواد");
    private void OnDebtTileClick(object sender, RoutedEventArgs e) => ShowPlaceholder("ذمم العملاء والموردين");
    private void OnBackupTileClick(object sender, RoutedEventArgs e) => ShowPlaceholder("تشغيل النسخ الاحتياطي");

    private void OnSaveExchangeRateClick(object sender, RoutedEventArgs e) => ShowPlaceholder("تم حفظ سعر الصرف");
    private void OnRefreshExchangeRateClick(object sender, RoutedEventArgs e) => ShowPlaceholder("تحديث أسعار الصرف");

    private void ShowPlaceholder(string message)
    {
        MessageBox.Show(message, "محاكاة", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
