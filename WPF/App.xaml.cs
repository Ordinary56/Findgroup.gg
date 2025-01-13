using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

    private readonly IServiceProvider _provider;
    private const string URL_BASE_ADDRESS = "http://localhost:5510/";
    public IHost Host { get; private set; }
    public App()
    {
        
    }
    public IHostBuilder CreateHostBuilder()
    {
        return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices(ServiceConfig)
            .ConfigureLogging(AddLogging)
            ;

    }

    /// <summary>
    /// -- THIS COMMENT NEEDS FURTHER DOCUMENTATION -- 
    /// List of all services that the app uses throught it's lifetime
    /// Different services has different lifetimes and scopes
    /// <list type="bullet">
    ///     <item>
    ///         <description>Singleton - Returns the same object per request</description>
    ///     </item>
    ///     <item>
    ///         <description>Transient - New object is created per request</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <param name="services">The host's service collection</param>
    public static void ServiceConfig(IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewmodel => (ViewModelBase)provider.GetRequiredService(viewmodel));
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<MainWindow>(provider => new MainWindow()
        {
            DataContext = provider.GetRequiredService<INavigationService>()
        });
        services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
        {
            client.BaseAddress = new Uri(URL_BASE_ADDRESS + "/Auth");
        }); 
    }
    public static void AddLogging(ILoggingBuilder builder)
    {
        builder.ClearProviders();
        builder.AddConsole();
        builder.AddDebug();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Host = CreateHostBuilder().Build();
        // this is still an anti-pattern due to manually settings CurrentViewModel, but this will do it 
        Host.Services.GetRequiredService<INavigationService>().CurrentViewModel = Host.Services.GetRequiredService<MainViewModel>();
        var MainWindow = Host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }
}

