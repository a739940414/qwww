using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Application.Interfaces;
using AlatabeSoft.Domain.Enums;
using AlatabeSoft.Presentation.Wpf.ViewModels.Commands;

namespace AlatabeSoft.Presentation.Wpf.ViewModels;

public class CashPaymentViewModel : ViewModelBase, IAsyncLoadable
{
    private readonly ICashManagementService _cashManagementService;
    private readonly IAccountsService _accountsService;
    private bool _isLoaded;
    private CashBoxLookupItem? _selectedCashBox;
    private AccountRowViewModel? _selectedExpenseAccount;
    private DateTime _paymentDate = DateTime.Today;
    private decimal _amount;
    private decimal _exchangeRate = 1m;
    private string _description = string.Empty;
    private string _referenceNumber = string.Empty;

    public CashPaymentViewModel(ICashManagementService cashManagementService, IAccountsService accountsService)
    {
        _cashManagementService = cashManagementService;
        _accountsService = accountsService;
        SavePaymentCommand = new AsyncRelayCommand(SavePaymentAsync, CanSavePayment);
    }

    public ObservableCollection<CashBoxLookupItem> CashBoxes { get; } = new();
    public ObservableCollection<AccountRowViewModel> ExpenseAccounts { get; } = new();
    public ObservableCollection<CashPaymentSummaryViewModel> Payments { get; } = new();

    public CashBoxLookupItem? SelectedCashBox
    {
        get => _selectedCashBox;
        set
        {
            if (SetProperty(ref _selectedCashBox, value))
            {
                if (value is not null)
                {
                    ExchangeRate = value.ExchangeRate;
                }

                SavePaymentCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public AccountRowViewModel? SelectedExpenseAccount
    {
        get => _selectedExpenseAccount;
        set
        {
            if (SetProperty(ref _selectedExpenseAccount, value))
            {
                SavePaymentCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public DateTime PaymentDate
    {
        get => _paymentDate;
        set
        {
            if (SetProperty(ref _paymentDate, value))
            {
                SavePaymentCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public decimal Amount
    {
        get => _amount;
        set
        {
            if (SetProperty(ref _amount, value))
            {
                SavePaymentCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public decimal ExchangeRate
    {
        get => _exchangeRate;
        set
        {
            if (SetProperty(ref _exchangeRate, value))
            {
                SavePaymentCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string ReferenceNumber
    {
        get => _referenceNumber;
        set => SetProperty(ref _referenceNumber, value);
    }

    public AsyncRelayCommand SavePaymentCommand { get; }

    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        if (_isLoaded)
        {
            return;
        }

        var cashBoxes = await _cashManagementService.GetCashBoxesAsync(cancellationToken);
        CashBoxes.Clear();
        foreach (var box in cashBoxes)
        {
            CashBoxes.Add(new CashBoxLookupItem(box.Id, box.Name, box.CurrencyId, box.CurrencyCode, box.ExchangeRate, box.AccountId));
        }

        SelectedCashBox = CashBoxes.FirstOrDefault();

        var expenseAccounts = await _accountsService.GetAccountsAsync(AccountType.Expense, cancellationToken);
        ExpenseAccounts.Clear();
        foreach (var account in expenseAccounts)
        {
            ExpenseAccounts.Add(new AccountRowViewModel(account.Id, account.Code, account.Name, account.Type, string.Empty));
        }

        SelectedExpenseAccount = ExpenseAccounts.FirstOrDefault();

        var payments = await _cashManagementService.GetRecentCashPaymentsAsync(10, cancellationToken);
        Payments.Clear();
        foreach (var payment in payments)
        {
            Payments.Add(new CashPaymentSummaryViewModel(payment.ReferenceNumber, payment.Date, payment.CashBoxName, payment.CurrencyCode, payment.Amount, payment.ExpenseAccountName, payment.Description));
        }

        _isLoaded = true;
    }

    private bool CanSavePayment() =>
        SelectedCashBox is not null &&
        SelectedExpenseAccount is not null &&
        PaymentDate != default &&
        Amount > 0 &&
        ExchangeRate > 0;

    private async Task SavePaymentAsync()
    {
        if (!CanSavePayment())
        {
            return;
        }

        var dto = new CashPaymentCreateDto(
            ReferenceNumber,
            PaymentDate,
            SelectedCashBox!.Id,
            SelectedCashBox.CurrencyId,
            Amount,
            ExchangeRate,
            SelectedExpenseAccount!.Id,
            Description,
            CreatedByUserId: 1);

        var summary = await _cashManagementService.CreateCashPaymentAsync(dto, CancellationToken.None);
        Payments.Insert(0, new CashPaymentSummaryViewModel(summary.ReferenceNumber, summary.Date, summary.CashBoxName, summary.CurrencyCode, summary.Amount, summary.ExpenseAccountName, summary.Description));
        ClearForm();
    }

    private void ClearForm()
    {
        ReferenceNumber = string.Empty;
        Amount = 0m;
        Description = string.Empty;
        PaymentDate = DateTime.Today;
    }
}

public record CashPaymentSummaryViewModel(string ReferenceNumber, DateTime Date, string CashBoxName, string CurrencyCode, decimal Amount, string ExpenseAccountName, string Description);
