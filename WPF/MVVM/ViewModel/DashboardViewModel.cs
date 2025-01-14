using Microsoft.Extensions.Logging;
using WPF.Core;
using WPF.Services;

namespace WPF.MVVM.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly INavigationService _navigation;
        private readonly ILogger<DashboardViewModel> _logger;
        public DashboardViewModel(INavigationService navigation, ILogger<DashboardViewModel> logger)
        {
            _navigation = navigation;
            _logger = logger;
        }
    }
}