using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WPF.Core;
using WPF.MVVM.ViewModel;
using WPF.Services;

namespace WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    IServiceProvider _provider;
    public App()
    {
        ServiceCollection collection = new();
        collection.AddSingleton<MainWindow>(provider => new()
        {
            DataContext = new MainViewModel()
        });
        collection.AddSingleton<INavigation, NavigationService>(provider => new(provider.GetRequiredService<Func<Type,ViewModelBase>>())
        {
            CurrentViewModel = new MainViewModel()
        });
        collection.AddSingleton<Func<Type, ViewModelBase>>(provider => viewmodel => (ViewModelBase)provider.GetRequiredService(viewmodel));
        collection.AddSingleton<MainViewModel>();

        _provider = collection.BuildServiceProvider();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow window = _provider.GetRequiredService<MainWindow>();
        base.OnStartup(e);
        window.Show();
    }
}

