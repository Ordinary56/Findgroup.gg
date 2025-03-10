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
    public partial class CreateNewUserDialogViewModel : ViewModelBase
    {
        #region Dependencies
        private readonly IUserRepostory _repo;
        private readonly ILogger<CreateNewUserDialogViewModel> _logger;
        #endregion

        #region Fields
        [ObservableProperty]
        private string? username;
        [ObservableProperty]
        private string? email;
        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private bool isSuccessfull = false;
        #endregion

        #region Commands
        AsyncRelayCommand CreateNewUserCommand { get; }
        #endregion

        public CreateNewUserDialogViewModel(IUserRepostory repo, ILogger<CreateNewUserDialogViewModel> logger)
        {
            CreateNewUserCommand = new(CreateNewUser);
            _repo = repo;
            _logger = logger;
        }

        private async Task CreateNewUser()
        {
            RegisterNewUserDTO dto = new()
            {
                userName = Username,
                email = Email,
                password = Password
            };
            _logger.LogInformation("Attempting to create new user with {user}", dto);
            try
            {
                await _repo.CreateNew(dto);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Something went wrong when creating a new user: {Reason}", ex.Message);
            }
        }


    }
}
