using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using WPF.Core;
using WPF.Helpers;
using WPF.MVVM.Model;
using WPF.MVVM.ViewModel;
using WPF.Services;
namespace WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string URL_BASE_ADDRESS = "http://localhost:5510/api";
    public IHost Host { get; private set; }
    public static IConfiguration Configuration { get; private set; } = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build()
   public static IHostBuilder CreateHostBuilder()
    {
        return Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureServices(ServiceConfig)
            .ConfigureLogging(AddLogging);

    }

    /// <summary>
    /// <para>-- THIS COMMENT NEEDS FURTHER DOCUMENTATION -- </para>
    /// <para>List of all services that the app uses throught it's lifetime
    /// Different services has different lifetimes and scopes</para>
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
        services.AddSingleton<IStorageHelper, StorageHelper>();
        services.AddSingleton(Configuration);
        services.AddSingleton<User>(provider =>
        {
            var storage = provider.GetRequiredService<IStorageHelper>();
            try
            {
                string data = storage.LoadData(@"./saved/data.txt");
                string[] fields = data.Split("-");
                return new User()
                {
                    Username = fields[0],
                    Password = fields[1],
                    AuthenticationToken = fields[2],
                    RefreshToken = fields[3]
                };
            }
            catch (Exception)
            {
                return new User();
            }
        });
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewmodel => (ViewModelBase)provider.GetRequiredService(viewmodel));
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<MainWindow>(provider => new MainWindow()
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });
        services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
        {
            client.BaseAddress = new Uri(URL_BASE_ADDRESS + "/Auth");
        }).SetHandlerLifetime(TimeSpan.FromMinutes(10));
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
    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        // Check if the User already has these properties set
        User user = Host.Services.GetRequiredService<User>();
        if (IsUserEmpty(ref user)) return;
        using FileStream stream = new(@"saved/data.json", FileMode.Create);
        await JsonSerializer.SerializeAsync(stream, user);
    }

    private static bool IsUserEmpty(ref User user)
    {
        return string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password);
    }

}

