using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Core;
using WPF.MVVM.Model.DTOs.Input;
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
        public bool isLoading;

        [ObservableProperty]
        private UserDTO? selectedUser;

        [ObservableProperty]
        private PostDTO? selectedPost;

        public AsyncRelayCommand<PostDTO?> DeletePostCommand { get; }
        public AsyncRelayCommand<UserDTO?> DeleteUserCommand { get; }
        public AsyncRelayCommand CreateNewUserCommand { get; }


        // Konstruktor, ami betölti az usereket
        public DashboardViewModel(INavigationService navigation, ILogger<DashboardViewModel> logger, IUserRepostory userRepository, IPostRepository postRepository)
        {
            _navigation = navigation;
            _logger = logger;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _ = LoadUsersAsync();
            _ = LoadPostsAsync();
            DeleteUserCommand = new(_userRepository.DeleteUser);
            DeletePostCommand = new(_postRepository.DeletePost);
            CreateNewUserCommand = new(async () =>
            {
                RegisterNewUserDTO newUser = new()
                {
                   id = Guid.NewGuid().ToString(),
                   userName = "asd",
                   email = "asd@asd.com",
                   password = "Passwd123$"
                };
                try
                {
                    await userRepository.CreateNew(newUser);
                    Users.Add(new UserDTO
                    {
                        UserName = newUser.userName,
                        Email = newUser.email,

                    });
                }
                catch(Exception ex)
                {
                    return;
                }
            });
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
       

        // Felhasználó szerkesztése
        public void EditUser()
        {
            if (SelectedUser is null) return;

            _logger.LogInformation("Editing user: {UserName}", selectedUser.UserName);
        }
        
    }
}
