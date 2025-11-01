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

public class AccountsViewModel : ViewModelBase, IAsyncLoadable
{
    private readonly IAccountsService _accountsService;
    private readonly ICurrencyService _currencyService;
    private bool _isLoaded;
    private string _accountCode = string.Empty;
    private string _accountName = string.Empty;
    private CurrencyLookupItem? _selectedCurrency;
    private AccountTypeOption? _selectedAccountType;
    private int? _selectedParentAccountId;

    public AccountsViewModel(IAccountsService accountsService, ICurrencyService currencyService)
    {
        _accountsService = accountsService;
        _currencyService = currencyService;
        SaveAccountCommand = new AsyncRelayCommand(SaveAccountAsync, CanSaveAccount);
    }

    public ObservableCollection<AccountRowViewModel> Accounts { get; } = new();
    public ObservableCollection<CurrencyLookupItem> Currencies { get; } = new();
    public ObservableCollection<AccountTypeOption> AccountTypes { get; } = new(
        Enum.GetValues<AccountType>().Select(type => new AccountTypeOption(type, GetAccountTypeDisplay(type))));

    public ObservableCollection<AccountRowViewModel> ParentAccounts { get; } = new();

    public string AccountCode
    {
        get => _accountCode;
        set
        {
            if (SetProperty(ref _accountCode, value))
            {
                SaveAccountCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string AccountName
    {
        get => _accountName;
        set
        {
            if (SetProperty(ref _accountName, value))
            {
                SaveAccountCommand.RaiseCanExecuteChanged();
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
                SaveAccountCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public AccountTypeOption? SelectedAccountType
    {
        get => _selectedAccountType;
        set
        {
            if (SetProperty(ref _selectedAccountType, value))
            {
                SaveAccountCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public int? SelectedParentAccountId
    {
        get => _selectedParentAccountId;
        set
        {
            if (SetProperty(ref _selectedParentAccountId, value))
            {
                SaveAccountCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public AsyncRelayCommand SaveAccountCommand { get; }

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

        var accounts = await _accountsService.GetAccountsAsync(null, cancellationToken);
        Accounts.Clear();
        ParentAccounts.Clear();
        foreach (var account in accounts)
        {
            var currency = currencies.First(c => c.Id == account.CurrencyId);
            var row = new AccountRowViewModel(account.Id, account.Code, account.Name, account.Type, currency.Code);
            Accounts.Add(row);
            ParentAccounts.Add(row);
        }

        SelectedAccountType = AccountTypes.FirstOrDefault();
        SelectedCurrency = Currencies.FirstOrDefault();

        _isLoaded = true;
    }

    private bool CanSaveAccount() =>
        !string.IsNullOrWhiteSpace(AccountCode) &&
        !string.IsNullOrWhiteSpace(AccountName) &&
        SelectedCurrency is not null &&
        SelectedAccountType is not null;

    private async Task SaveAccountAsync()
    {
        if (!CanSaveAccount())
        {
            return;
        }

        var dto = new AccountDto(
            0,
            AccountCode.Trim(),
            AccountName.Trim(),
            SelectedAccountType!.Value,
            SelectedCurrency!.Id,
            SelectedParentAccountId,
            null,
            null,
            true);

        try
        {
            var created = await _accountsService.CreateAsync(dto, CancellationToken.None);
            var currency = Currencies.First(c => c.Id == created.CurrencyId);
            var row = new AccountRowViewModel(created.Id, created.Code, created.Name, created.Type, currency.Code);
            Accounts.Add(row);
            ParentAccounts.Add(row);
            ClearForm();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ClearForm()
    {
        AccountCode = string.Empty;
        AccountName = string.Empty;
        SelectedParentAccountId = null;
    }

    public static string GetAccountTypeDisplay(AccountType type) => type switch
    {
        AccountType.Asset => "أصل",
        AccountType.Liability => "التزامات",
        AccountType.Equity => "حقوق ملكية",
        AccountType.Revenue => "إيراد",
        AccountType.Expense => "مصروف",
        AccountType.ContraAsset => "حساب عكسي للأصول",
        AccountType.ContraLiability => "حساب عكسي للالتزامات",
        _ => type.ToString()
    };
}

public record CurrencyLookupItem(int Id, string Code, string Name)
{
    public override string ToString() => $"{Code} - {Name}";
}

public record AccountTypeOption(AccountType Value, string DisplayName)
{
    public override string ToString() => DisplayName;
}

public record AccountRowViewModel(int Id, string Code, string Name, AccountType Type, string CurrencyCode)
{
    public string DisplayType => AccountsViewModel.GetAccountTypeDisplay(Type);
    public string DisplayName => string.IsNullOrWhiteSpace(Code) ? Name : $"{Code} - {Name}";
}
