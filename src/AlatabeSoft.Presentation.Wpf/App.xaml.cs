using System;
using System.Windows;
using AlatabeSoft.Infrastructure;
using AlatabeSoft.Infrastructure.Persistence;
using AlatabeSoft.Presentation.Wpf.ViewModels;
using AlatabeSoft.Presentation.Wpf.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AlatabeSoft.Presentation.Wpf;

public partial class App : System.Windows.Application
{
    private IHost? _host;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(AppContext.BaseDirectory);
                builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddInfrastructure(context.Configuration);

                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddTransient<DashboardViewModel>();
                services.AddTransient<AccountsViewModel>();
                services.AddTransient<CustomersViewModel>();
                services.AddTransient<CashReceiptViewModel>();
                services.AddTransient<CashPaymentViewModel>();
            })
            .Build();

        _host.Start();

        using var scope = _host.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        seeder.SeedAsync().GetAwaiter().GetResult();

        var mainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
        MainWindow = mainWindow;
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (_host is not null)
        {
            _host.StopAsync(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();
            _host.Dispose();
        }

        base.OnExit(e);
    }
}
