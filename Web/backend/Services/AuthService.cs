using AutoMapper;
using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Findgroup_Backend.Services
{
    public class AuthService(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        ITokenService service,
        ITokenRepository tokenRepo,
        IMapper mapper) : IAuthService
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _token = service;
        private readonly ITokenRepository _tokenRepo = tokenRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<AuthResult> LoginUser(LoginDTO credentials)
        {
            var result = await _signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, true, true);
            if (!result.Succeeded)
            {
                throw new AuthenticationFailureException($"Failed to Authenticate User");
            }
            var user = await _userManager.FindByNameAsync(credentials.Username) ??
                throw new AuthenticationFailureException("Requested User is null");

            var token = await _token.GenerateAccessToken(user);
            var refreshToken = _token.GenerateRefreshToken();
            if (user.RefreshToken != null && user.RefreshToken.IsRevoked)
            {
                _tokenRepo.RemoveToken(user.RefreshToken);
                await _tokenRepo.AddToken(refreshToken);
                user.RefreshToken = refreshToken;
            }
            await _userManager.UpdateAsync(user);
            return new AuthResult()
            {
                Token = token,
                RefreshToken = refreshToken.TokenHash
            };
        }

        public async Task<IdentityResult> RegisterUser(UserDTO newUser)
        {
            User mappedUser = _mapper.Map<User>(newUser);
            var result = await _userManager.CreateAsync(mappedUser, newUser.Password!);
            await _userManager.AddToRoleAsync(mappedUser, "User");
            return result;
        }
        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
