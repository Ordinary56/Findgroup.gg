using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
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
        private readonly IPostRepository _postRepository;
        private readonly IGroupRepository _groupRepository;

        public ObservableCollection<UserDTO> Users { get; } = new();
        public ObservableCollection<PostDTO> Posts { get; } = new();
        public ObservableCollection<GroupDTO> Groups { get; } = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private UserDTO? selectedUser;

        [ObservableProperty]
        private PostDTO? selectedPost;


        // Konstruktor, ami betölti az usereket
        public DashboardViewModel(INavigationService navigation, ILogger<DashboardViewModel> logger, IUserRepostory userRepository, IPostRepository postRepository)
        {
            _navigation = navigation;
            _logger = logger;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _ = LoadUsersAsync();
            _ = LoadPostsAsync();
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
                _logger.LogInformation("First user: {User}", JsonSerializer.Serialize(Users.FirstOrDefault()));

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
        private async Task LoadPostsAsync()
        {
            IsLoading = true; // Betöltési állapot
            try
            {
                await foreach (var post in _postRepository.GetPost())
                {
                    Posts.Add(post);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load posts");
            }
            finally
            {
                IsLoading = false; // Betöltés vége
            }
        }
        private async Task LoadGroupsAsync()
        {
            IsLoading = true; // Betöltési állapot
            try
            {
                await foreach (var group in _groupRepository.GetGroups())
                {
                    Groups.Add(group);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load groups");
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
        [RelayCommand]
        private async Task DeletePost()
        {
            if (selectedPost is null) return;

            try
            {
                await _postRepository.DeletePost(selectedPost);
                Posts.Remove(selectedPost); // eltávolítjuk a listából
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete post");
            }
        }

        // Felhasználó szerkesztése
        [RelayCommand]
        private void EditUser()
        {
            if (SelectedUser is null) return;

            _logger.LogInformation("Editing user: {UserName}", selectedUser.userName);
        }
    }
}
