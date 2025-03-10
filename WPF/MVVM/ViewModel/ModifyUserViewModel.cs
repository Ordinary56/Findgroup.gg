using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Core;
using WPF.MVVM.Model.DTOs.Input;
using WPF.Repositories;

namespace WPF.MVVM.ViewModel
{
    public partial class ModifyUserViewModel : ViewModelBase
    {
        private readonly ILogger<ModifyUserViewModel> _logger;
        private readonly IUserRepostory _repo;

        [ObservableProperty]
        private string? username;

        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? password;

        private string? id;

        public AsyncRelayCommand ModifyUserCommand { get; }

        public ModifyUserViewModel(ILogger<ModifyUserViewModel> logger, IUserRepostory repo, string userId)
        {
            _logger = logger;
            _repo = repo;
            ModifyUserCommand = new(ModifyUser);
            id = userId;
        }

        private async Task ModifyUser()
        {
            RegisterNewUserDTO dto = new()
            {
                id = id,
                userName = Username,
                password = Password,
                email = Email
            };
            _logger.LogInformation("Attempting to modify user with {dto}", dto);
            try
            {
                await _repo.ModifyUser(dto);

            }
            catch (Exception ex) 
            {
                _logger.LogError("Modify User failed: {Reason}", ex.Message);
            }
        }
    }
}
