using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WPF.Core;
using WPF.MVVM.Model.DTOs.Output;
using WPF.Repositories;
using WPF.Repositories.Interfaces;
using WPF.Services.Interfaces;

namespace WPF.MVVM.ViewModel
{
    public partial class DashboardViewModel : ViewModelBase
    {
        private readonly INavigationService _navigation;
        private readonly ILogger<DashboardViewModel> _logger;
        private readonly IUserRepostory _userRepository;

        public ObservableCollection<UserDTO> Users { get; } = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private UserDTO? selectedUser;


        // Konstruktor, ami betölti az usereket
        public DashboardViewModel(INavigationService navigation, ILogger<DashboardViewModel> logger, IUserRepostory userRepository)
        {
            _navigation = navigation;
            _logger = logger;
            _userRepository = userRepository;

            _ = LoadUsersAsync();
        }

        // Users betöltése az API-ból
        private async Task LoadUsersAsync()
        {
            IsLoading = true; // Betöltési állapot
            try
            {
                await foreach (var user in _userRepository.GetUsers())
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load users");
            }
            finally
            {
                IsLoading = false; // Betöltés vége
            }
        }

        // Felhasználó törlése
        [RelayCommand]
        private async Task DeleteUser()
        {
            if (SelectedUser is null) return;

            try
            {
                await _userRepository.DeleteUser(SelectedUser);
                Users.Remove(SelectedUser); // eltávolítjuk a listából
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete user");
            }
        }

        // Felhasználó szerkesztése
        [RelayCommand]
        private void EditUser()
        {
            if (SelectedUser is null) return;

            _logger.LogInformation("Editing user: {UserName}", SelectedUser.username);
            // Itt jöhet popup vagy navigáció
        }
    }
}
