using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security;
using WPF.Core;
using WPF.MVVM.Model;
using WPF.Services;

namespace WPF.MVVM.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigation;
        private readonly ILogger<MainViewModel> _logger;
        private readonly IAuthenticationService _authenticationService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private SecureString password;
        public AsyncRelayCommand AuthenticateCommand { get; }
        public INavigationService Navigation => _navigation;
        public MainViewModel(INavigationService navigation, IAuthenticationService authentication,ILogger<MainViewModel> logger)
        {
            _navigation = navigation;
            _logger = logger;
            _authenticationService = authentication;
            AuthenticateCommand = new(AuthenticateAsync);
        }



        private async Task AuthenticateAsync()
        {
            User user = new()
            {
                Username = username ?? string.Empty,
                Password = password.ToString() ?? string.Empty
            };
            bool res = await _authenticationService.Authenticate(user);
        }
    }
}
