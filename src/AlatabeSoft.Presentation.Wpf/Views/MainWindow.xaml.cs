using System.Windows;
using AlatabeSoft.Presentation.Wpf.ViewModels;

namespace AlatabeSoft.Presentation.Wpf.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
