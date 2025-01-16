using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Core;
using WPF.MVVM.Model;
using WPF.MVVM.ViewModel;
using WPF.Services;
using Xunit;

namespace WPF.Tests
{
    public class MainViewModel_Test
    {
        private readonly Mock<INavigationService> _navService;
        private readonly Mock<IAuthenticationService> _authService;
        private readonly Mock<ILogger<MainViewModel>> _logger;
        private readonly MainViewModel _mainViewModel;
        public MainViewModel_Test()
        {
            _navService = new();
            _authService = new();
            _logger = new();
            _mainViewModel = new(_navService.Object, _authService.Object, _logger.Object);
        }
        [Fact]
        public void Login_Returns_False()
        {
            User badUser = new()
            {
                Username = "",
                Password = ""
            };
            // TODO: implement unit test 
            Assert.False(false);
        }

        [Fact]
        public void Login_Returns_True()
        {
            // TODO: Implement unit test
            Assert.True(true);
        }
    }
}
