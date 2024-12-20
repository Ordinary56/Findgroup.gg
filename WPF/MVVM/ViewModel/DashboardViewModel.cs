using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF.Core;
using WPF.Services;

namespace WPF.MVVM.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly INavigation _navigation;
        private readonly ILogger<DashboardViewModel> _logger;
        public DashboardViewModel(INavigation navigation, ILogger<DashboardViewModel> logger)
        {
            _navigation = navigation;
            _logger = logger;
        }
    }
}