using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlatabeSoft.Presentation.Wpf.ViewModels.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AlatabeSoft.Presentation.Wpf.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private NavigationItemViewModel? _selectedNavigationItem;
    private ViewModelBase? _currentViewModel;

    public ObservableCollection<NavigationItemViewModel> NavigationItems { get; }

    public string SelectedBranch { get; set; } = "الفرع الرئيسي";
    public string CurrentUser { get; set; } = "محمد الأحمد";

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        private set => SetProperty(ref _currentViewModel, value);
    }

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        NavigationItems = new ObservableCollection<NavigationItemViewModel>
        {
            CreateNavigationItem("🏠", "الرئيسية", () => _serviceProvider.GetRequiredService<DashboardViewModel>()),
            CreateNavigationItem("💼", "الحسابات", () => _serviceProvider.GetRequiredService<AccountsViewModel>()),
            CreateNavigationItem("🧑‍🤝‍🧑", "العملاء والموردون", () => _serviceProvider.GetRequiredService<CustomersViewModel>()),
            CreateNavigationItem("💰", "سندات القبض", () => _serviceProvider.GetRequiredService<CashReceiptViewModel>()),
            CreateNavigationItem("🧾", "سندات الصرف", () => _serviceProvider.GetRequiredService<CashPaymentViewModel>())
        };

        ActivateAsync(NavigationItems.First()).GetAwaiter().GetResult();
    }

    private NavigationItemViewModel CreateNavigationItem(string icon, string title, Func<ViewModelBase> viewModelFactory)
    {
        NavigationItemViewModel? item = null;
        item = new NavigationItemViewModel(icon, title, viewModelFactory, () => ActivateAsync(item!));
        return item;
    }

    private async Task ActivateAsync(NavigationItemViewModel item)
    {
        if (_selectedNavigationItem == item)
        {
            return;
        }

        if (_selectedNavigationItem is not null)
        {
            _selectedNavigationItem.IsSelected = false;
        }

        _selectedNavigationItem = item;
        _selectedNavigationItem.IsSelected = true;

        var viewModel = item.GetOrCreateViewModel();

        if (!item.IsInitialized && viewModel is IAsyncLoadable loadable)
        {
            await loadable.LoadAsync(CancellationToken.None);
            item.MarkInitialized();
        }

        CurrentViewModel = viewModel;
    }
}
