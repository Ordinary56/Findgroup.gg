using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security;
using WPF.Core;
using WPF.Services;

namespace WPF.MVVM.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigation;
        private readonly ILogger<MainViewModel> _logger;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private SecureString password;
        public AsyncRelayCommand AuthenticateCommand { get; }
        public INavigationService Navigation => _navigation;
        public MainViewModel(INavigationService navigation, ILogger<MainViewModel> logger)
        {
            _navigation = navigation;
            _logger = logger;
            AuthenticateCommand = new(AuthenticateAsync);
        }



        private async Task AuthenticateAsync()
        {
            // TODO: Refactor this method and use IAuthenticateService
            await Task.CompletedTask;
        }
    }
}
