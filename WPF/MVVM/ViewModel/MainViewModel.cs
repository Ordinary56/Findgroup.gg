using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using WPF.Core;
using WPF.Services;

namespace WPF.MVVM.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly INavigation _navigation;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MainViewModel> _logger;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;
        public AsyncRelayCommand AuthenticateCommand { get; }
        public INavigation Navigation => _navigation;
        public MainViewModel(INavigation navigation, HttpClient client, ILogger<MainViewModel> logger)
        {
            _navigation = navigation;
            _navigation.CurrentViewModel = this;
            _httpClient = client;
            _logger = logger;
            AuthenticateCommand = new(AuthenticateAsync);
        }



        private async Task AuthenticateAsync()
        {
            _logger.LogInformation("Started sending login details");
            _httpClient.BaseAddress = new Uri("http://localhost:5510");
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/Auth/login", new
            {
                Username = username,
                Password = password
            });
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogError("Failed to Authenticate: {Reason}", response.Content);
                return;
            }
            _navigation.MoveTo<DashboardViewModel>();
        }
    }
}
