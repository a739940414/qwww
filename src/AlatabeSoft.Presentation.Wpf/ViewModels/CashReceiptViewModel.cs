using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlatabeSoft.Application.DTOs;
using AlatabeSoft.Application.Interfaces;
using AlatabeSoft.Presentation.Wpf.ViewModels.Commands;

namespace AlatabeSoft.Presentation.Wpf.ViewModels;

public class CashReceiptViewModel : ViewModelBase, IAsyncLoadable
{
    private readonly ICashManagementService _cashManagementService;
    private bool _isLoaded;
    private CashBoxLookupItem? _selectedCashBox;
    private BusinessEntityLookupItem? _selectedCustomer;
    private DateTime _receiptDate = DateTime.Today;
    private decimal _amount;
    private decimal _exchangeRate = 1m;
    private string _description = string.Empty;
    private string _referenceNumber = string.Empty;

    public CashReceiptViewModel(ICashManagementService cashManagementService)
    {
        _cashManagementService = cashManagementService;
        SaveReceiptCommand = new AsyncRelayCommand(SaveReceiptAsync, CanSaveReceipt);
    }

    public ObservableCollection<CashBoxLookupItem> CashBoxes { get; } = new();
    public ObservableCollection<BusinessEntityLookupItem> Customers { get; } = new();
    public ObservableCollection<CashReceiptSummaryViewModel> Receipts { get; } = new();

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

                SaveReceiptCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public BusinessEntityLookupItem? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            if (SetProperty(ref _selectedCustomer, value))
            {
                SaveReceiptCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public DateTime ReceiptDate
    {
        get => _receiptDate;
        set
        {
            if (SetProperty(ref _receiptDate, value))
            {
                SaveReceiptCommand.RaiseCanExecuteChanged();
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
                SaveReceiptCommand.RaiseCanExecuteChanged();
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
                SaveReceiptCommand.RaiseCanExecuteChanged();
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

    public AsyncRelayCommand SaveReceiptCommand { get; }

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

        var customers = await _cashManagementService.GetCustomersAsync(cancellationToken);
        Customers.Clear();
        foreach (var customer in customers)
        {
            Customers.Add(new BusinessEntityLookupItem(customer.Id, customer.Name, customer.CurrencyCode));
        }

        SelectedCustomer = Customers.FirstOrDefault();

        var receipts = await _cashManagementService.GetRecentCashReceiptsAsync(10, cancellationToken);
        Receipts.Clear();
        foreach (var receipt in receipts)
        {
            Receipts.Add(new CashReceiptSummaryViewModel(receipt.ReferenceNumber, receipt.Date, receipt.CashBoxName, receipt.CurrencyCode, receipt.Amount, receipt.CustomerName, receipt.Description));
        }

        _isLoaded = true;
    }

    private bool CanSaveReceipt() =>
        SelectedCashBox is not null &&
        SelectedCustomer is not null &&
        ReceiptDate != default &&
        Amount > 0 &&
        ExchangeRate > 0;

    private async Task SaveReceiptAsync()
    {
        if (!CanSaveReceipt())
        {
            return;
        }

        var dto = new CashReceiptCreateDto(
            ReferenceNumber,
            ReceiptDate,
            SelectedCashBox!.Id,
            SelectedCashBox.CurrencyId,
            Amount,
            ExchangeRate,
            SelectedCustomer?.Id,
            Description,
            CreatedByUserId: 1);

        var summary = await _cashManagementService.CreateCashReceiptAsync(dto, CancellationToken.None);
        Receipts.Insert(0, new CashReceiptSummaryViewModel(summary.ReferenceNumber, summary.Date, summary.CashBoxName, summary.CurrencyCode, summary.Amount, summary.CustomerName, summary.Description));
        ClearForm();
    }

    private void ClearForm()
    {
        ReferenceNumber = string.Empty;
        Amount = 0m;
        Description = string.Empty;
        ReceiptDate = DateTime.Today;
    }
}

public record CashBoxLookupItem(int Id, string Name, int CurrencyId, string CurrencyCode, decimal ExchangeRate, int AccountId)
{
    public override string ToString() => $"{Name} ({CurrencyCode})";
}

public record BusinessEntityLookupItem(int Id, string Name, string CurrencyCode)
{
    public override string ToString() => Name;
}

public record CashReceiptSummaryViewModel(string ReferenceNumber, DateTime Date, string CashBoxName, string CurrencyCode, decimal Amount, string? CustomerName, string Description);
