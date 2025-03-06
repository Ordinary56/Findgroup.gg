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


        // Konstruktor, ami bet�lti az usereket
        public DashboardViewModel(INavigationService navigation, ILogger<DashboardViewModel> logger, IUserRepostory userRepository)
        {
            _navigation = navigation;
            _logger = logger;
            _userRepository = userRepository;

            _ = LoadUsersAsync();
        }

        // Users bet�lt�se az API-b�l
        private async Task LoadUsersAsync()
        {
            IsLoading = true; // Bet�lt�si �llapot
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
                IsLoading = false; // Bet�lt�s v�ge
            }
        }

        // Felhaszn�l� t�rl�se
        [RelayCommand]
        private async Task DeleteUser()
        {
            if (SelectedUser is null) return;

            try
            {
                await _userRepository.DeleteUser(SelectedUser);
                Users.Remove(SelectedUser); // elt�vol�tjuk a list�b�l
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete user");
            }
        }

        // Felhaszn�l� szerkeszt�se
        [RelayCommand]
        private void EditUser()
        {
            if (SelectedUser is null) return;

            _logger.LogInformation("Editing user: {UserName}", SelectedUser.username);
            // Itt j�het popup vagy navig�ci�
        }
    }
}
