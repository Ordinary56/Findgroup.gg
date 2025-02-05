using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Findgroup_Backend.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Logs in the user
        /// </summary>
        /// <param name="credentials">the User's username and password</param>
        /// <returns>A new access and refresh token</returns>
        public Task<AuthResult> LoginUser(LoginDTO credentials);
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="newUser">User's username, password and email</param>
        /// <returns></returns>
        public Task<IdentityResult> RegisterUser(UserDTO newUser);
        /// <summary>
        /// Logs out the user
        /// </summary>
        /// <returns>The completed Task</returns>
        public Task LogoutUser();
    }
}
