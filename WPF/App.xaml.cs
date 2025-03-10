using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Navigation;
using WPF.Core;
using WPF.Helpers;
using WPF.MVVM.Model;
using WPF.MVVM.ViewModel;
using WPF.Repositories;
using WPF.Repositories.Interfaces;
using WPF.Services;
using WPF.Services.Interfaces;
using NavigationService = WPF.Services.NavigationService;
namespace WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IHost Host { get; private set; }
    public static IConfiguration Configuration { get; private set; }
    private static IHostBuilder CreateHostBuilder()
    {
        return Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureServices(ServiceConfig)
            .ConfigureLogging(AddLogging);

    }
    private static IConfigurationBuilder CreateConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
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
        CookieContainer container = new();
        services.AddSingleton<IStorageHelper, StorageHelper>();
        services.AddSingleton(Configuration);
        services.AddSingleton<HttpClientHandler>(provider => {
            return new HttpClientHandler()
            {
                CookieContainer = container
            };
        });
        services.AddSingleton(provider =>
        {
            var storage = provider.GetRequiredService<IStorageHelper>();
            try
            {
                string data = storage.LoadData(@"./saved/data.txt");
                string[] fields = data.Split("-");
                return new AdminUser();
            }
            catch (Exception)
            {
                return new AdminUser();
            }
        });
        services.AddScoped<IUserRepostory, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewmodel => (ViewModelBase)provider.GetRequiredService(viewmodel));
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<MainWindow>(provider => new MainWindow()
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });
        services.AddHttpClient<IAuthenticationService, AuthenticationService>()
            .ConfigurePrimaryHttpMessageHandler(builder =>
            {
                return builder.GetRequiredService<HttpClientHandler>();
            }).ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(Configuration["AppSettings:ApiUrl"]!  + "/Auth/login");
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(10));

        services.AddHttpClient<IUserRepostory, UserRepository>()
          .ConfigurePrimaryHttpMessageHandler(builder =>
          {
              return builder.GetRequiredService<HttpClientHandler>();
          }).ConfigureHttpClient(client =>
          {
              client.BaseAddress = new Uri(Configuration["AppSettings:ApiUrl"]!);
          })
          .SetHandlerLifetime(TimeSpan.FromMinutes(10));
        services.AddHttpClient<IPostRepository, PostRepository>()
        .ConfigurePrimaryHttpMessageHandler(builder =>
        {
            return builder.GetRequiredService<HttpClientHandler>();
        }).ConfigureHttpClient(client =>
        {
            client.BaseAddress = new Uri(Configuration["AppSettings:ApiUrl"]!);
        })
        .SetHandlerLifetime(TimeSpan.FromMinutes(10));
        services.AddSingleton(container);
    }
    public static void AddLogging(ILoggingBuilder builder)
    {
        builder.ClearProviders();
        builder.AddConsole();
        builder.AddDebug();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        // Konzol megnyitása debug módban
        WPF.Helpers.ConsoleHelper.CreateConsole();

        try
        {
            Configuration = CreateConfiguration().Build();
            Host = CreateHostBuilder().Build();
            base.OnStartup(e);

            // Beállítjuk az első ViewModel-t manuálisan (ezt MVVM-nél érdemes máshogy kezelni)
            Host.Services.GetRequiredService<INavigationService>().CurrentViewModel = Host.Services.GetRequiredService<MainViewModel>();

            // Ablak megjelenítése
            var mainWindow = Host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            Console.WriteLine("WPF alkalmazás elindult."); // Debug log
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Az alkalmazás indításakor hiba történt: {ex.Message}");
        }
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        try
        {
            base.OnExit(e);

            //  Konzol bezárása
            WPF.Helpers.ConsoleHelper.CloseConsole();

            // User adatainak mentése
            AdminUser user = Host.Services.GetRequiredService<AdminUser>();
            if (!IsUserEmpty(ref user))
            {
                using FileStream stream = new(@"saved/data.json", FileMode.Create);
                await JsonSerializer.SerializeAsync(stream, user);
                Console.WriteLine("Felhasználói adatok mentve.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Kilépéskor hiba történt: {ex.Message}");
        }
    }


    private static bool IsUserEmpty(ref AdminUser user)
    {
        return string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password);
    }

}

