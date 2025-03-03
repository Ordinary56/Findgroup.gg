using Microsoft.Extensions.Logging;
using WPF.Core;
using WPF.Services;
using WPF.Services.Interfaces;

namespace WPF.MVVM.ViewModel
{
    public partial class DashboardViewModel(INavigationService navigation, ILogger<DashboardViewModel> logger) : ViewModelBase
    {
        private readonly INavigationService _navigation = navigation;
        private readonly ILogger<DashboardViewModel> _logger = logger;
    }
}