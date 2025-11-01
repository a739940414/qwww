using System;
using System.Threading.Tasks;
using AlatabeSoft.Presentation.Wpf.ViewModels.Commands;

namespace AlatabeSoft.Presentation.Wpf.ViewModels;

public class NavigationItemViewModel : ViewModelBase
{
    private readonly Func<ViewModelBase> _viewModelFactory;
    private ViewModelBase? _cachedViewModel;
    private bool _isSelected;

    public NavigationItemViewModel(string icon, string title, Func<ViewModelBase> viewModelFactory, Func<Task> activateAsync)
    {
        Icon = icon;
        Title = title;
        _viewModelFactory = viewModelFactory;
        SelectCommand = new AsyncRelayCommand(activateAsync);
    }

    public string Icon { get; }
    public string Title { get; }
    public AsyncRelayCommand SelectCommand { get; }
    public bool IsInitialized { get; private set; }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public ViewModelBase GetOrCreateViewModel() => _cachedViewModel ??= _viewModelFactory();

    public void MarkInitialized() => IsInitialized = true;
}
