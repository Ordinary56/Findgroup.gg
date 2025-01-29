using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Findgroup_Backend.Services
{
    public interface IAuthService
    {
        public Task<AuthResult> LoginUser(LoginDTO credentials);
        public Task<IdentityResult> RegisterUser(UserDTO newUser);
        public Task LogoutUser();
    }
}
