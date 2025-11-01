using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Application.Interfaces;
using AlatabeSoft.Domain.Enums;
using AlatabeSoft.Presentation.Wpf.ViewModels.Commands;

namespace AlatabeSoft.Presentation.Wpf.ViewModels;

public class CustomersViewModel : ViewModelBase, IAsyncLoadable
{
    private readonly IBusinessEntityService _businessEntityService;
    private readonly ICurrencyService _currencyService;
    private bool _isLoaded;
    private BusinessEntityTypeOption? _selectedEntityType;
    private CurrencyLookupItem? _selectedCurrency;
    private string _name = string.Empty;
    private string _phone = string.Empty;
    private string _email = string.Empty;
    private string _address = string.Empty;
    private decimal _creditLimit;

    public CustomersViewModel(IBusinessEntityService businessEntityService, ICurrencyService currencyService)
    {
        _businessEntityService = businessEntityService;
        _currencyService = currencyService;
        SaveEntityCommand = new AsyncRelayCommand(SaveAsync, CanSave);
        EntityTypes = new ObservableCollection<BusinessEntityTypeOption>(
            Enum.GetValues<BusinessEntityType>().Select(type => new BusinessEntityTypeOption(type, GetDisplayName(type))));
    }

    public ObservableCollection<BusinessEntityTypeOption> EntityTypes { get; }
    public ObservableCollection<CurrencyLookupItem> Currencies { get; } = new();
    public ObservableCollection<BusinessEntityListItem> Entities { get; } = new();

    public BusinessEntityTypeOption? SelectedEntityType
    {
        get => _selectedEntityType;
        set
        {
            if (SetProperty(ref _selectedEntityType, value))
            {
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public CurrencyLookupItem? SelectedCurrency
    {
        get => _selectedCurrency;
        set
        {
            if (SetProperty(ref _selectedCurrency, value))
            {
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (SetProperty(ref _name, value))
            {
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string Address
    {
        get => _address;
        set => SetProperty(ref _address, value);
    }

    public decimal CreditLimit
    {
        get => _creditLimit;
        set
        {
            if (SetProperty(ref _creditLimit, value))
            {
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public AsyncRelayCommand SaveEntityCommand { get; }

    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        if (_isLoaded)
        {
            return;
        }

        var currencies = await _currencyService.GetCurrenciesAsync(cancellationToken);
        Currencies.Clear();
        foreach (var currency in currencies)
        {
            Currencies.Add(new CurrencyLookupItem(currency.Id, currency.Code, currency.Name));
        }

        var entities = await _businessEntityService.GetAsync(null, cancellationToken);
        Entities.Clear();
        foreach (var entity in entities)
        {
            Entities.Add(new BusinessEntityListItem(
                entity.Id,
                entity.Type,
                entity.Name,
                entity.Phone,
                entity.Email,
                entity.CurrencyCode,
                entity.CreditLimit,
                entity.Balance));
        }

        SelectedEntityType = EntityTypes.FirstOrDefault();
        SelectedCurrency = Currencies.FirstOrDefault();

        _isLoaded = true;
    }

    private bool CanSave()
    {
        return SelectedEntityType is not null &&
               SelectedCurrency is not null &&
               !string.IsNullOrWhiteSpace(Name);
    }

    private async Task SaveAsync()
    {
        if (!CanSave())
        {
            return;
        }

        var dto = new BusinessEntityCreateDto(
            SelectedEntityType!.Value,
            Name,
            string.IsNullOrWhiteSpace(Phone) ? null : Phone,
            string.IsNullOrWhiteSpace(Email) ? null : Email,
            string.IsNullOrWhiteSpace(Address) ? null : Address,
            SelectedCurrency!.Id,
            CreditLimit,
            ControlAccountId: null);

        try
        {
            var created = await _businessEntityService.CreateAsync(dto, CancellationToken.None);
            Entities.Add(new BusinessEntityListItem(
                created.Id,
                created.Type,
                created.Name,
                created.Phone,
                created.Email,
                created.CurrencyCode,
                created.CreditLimit,
                created.Balance));
            ClearForm();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ClearForm()
    {
        Name = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        Address = string.Empty;
        CreditLimit = 0m;
    }

    private static string GetDisplayName(BusinessEntityType type) => type switch
    {
        BusinessEntityType.Customer => "عميل",
        BusinessEntityType.Supplier => "مورد",
        _ => type.ToString()
    };
}

public record BusinessEntityTypeOption(BusinessEntityType Value, string DisplayName)
{
    public override string ToString() => DisplayName;
}

public record BusinessEntityListItem(
    int Id,
    BusinessEntityType Type,
    string Name,
    string? Phone,
    string? Email,
    string CurrencyCode,
    decimal CreditLimit,
    decimal Balance)
{
    public string TypeDisplay => Type switch
    {
        BusinessEntityType.Customer => "عميل",
        BusinessEntityType.Supplier => "مورد",
        _ => Type.ToString()
    };
}
