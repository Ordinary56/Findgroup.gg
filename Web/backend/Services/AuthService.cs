using Findgroup_Backend.Helpers;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Findgroup_Backend.Services
{
    public class AuthService(SignInManager<User> signInManager, UserManager<User> userManager, ITokenHandler handler) : IAuthService
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenHandler _tokenHandler = handler;
        public async Task<AuthResult> LoginUser(LoginDTO credentials)
        {
            var result = await _signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, true, true);
            if (!result.Succeeded) 
            {
                throw new AuthenticationFailureException($"Failed to Authenticate User");
            }
            var user = await _userManager.FindByNameAsync(credentials.Username) ?? throw new AuthenticationFailureException("Requested User is null");
            List<Claim> authClaims = new()
            {
                new(ClaimTypes.Name, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ),
                new(ClaimTypes.NameIdentifier, user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles) authClaims.Add(new(ClaimTypes.Role, role));
            var token = _tokenHandler.GenerateAccessToken(authClaims);
            var refreshToken = _tokenHandler.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);
            return new AuthResult() 
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }

        public async Task<IdentityResult> RegisterUser(UserDTO newUser)
        {
            User mappedUser = new();
            var result = await _userManager.CreateAsync(mappedUser, newUser.Password);
            return result;
        }
        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
