using Findgroup_Backend.Models.DTOs.Input;
using Microsoft.AspNetCore.Identity;

namespace Findgroup_Backend.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResult> LoginUser(LoginDTO credentials);
        public Task<IdentityResult> RegisterUser(RegisterUserDTO newUser);
        public Task LogoutUser();
    }
}
