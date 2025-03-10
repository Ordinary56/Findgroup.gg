using AutoMapper;
using Findgroup_Backend.Data;
using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Input;
using Findgroup_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Findgroup_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _token;
        private readonly ITokenRepository _tokenRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly UserManager<User> _userManager;

        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, ITokenService token, ITokenRepository tokenRepo, IMapper mapper, ILogger<AuthService> logger, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _token = token;
            _tokenRepo = tokenRepo;
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }

        public async Task<AuthResult> LoginUser(LoginDTO credentials)
        {
            var result = await _signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, true, true);
            if (!result.Succeeded)
            {
                _logger.LogError("User Authentication failed. Either the username or the password was incorrent or something happend");
                throw new AuthenticationFailureException($"Failed to Authenticate User");
            }
            // HACK: userManager loads the user in without the navigation properties
            var user = await _context.Users.Include(u => u.RefreshToken).FirstAsync(u => u.UserName == credentials.Username);
            _logger.LogInformation("user.refresh != null {condition}", user.RefreshToken != null);
            var token = await _token.GenerateAccessToken(user);
            var refreshToken = _token.GenerateRefreshToken(user);
            if (user.RefreshToken != null)
            {
                _tokenRepo.RemoveToken(user.RefreshToken);
                user.RefreshToken = refreshToken;
                await _tokenRepo.AddToken(refreshToken);
            }
            else
            {
                _logger.LogInformation("RefreshToken: {token}", refreshToken);
                user.RefreshToken = refreshToken;
                await _tokenRepo.AddToken(refreshToken);
            }
            _logger.LogInformation("successfully added refresh token to user: {token}", user.RefreshToken.TokenHash);
            return new AuthResult()
            {
                Token = token,
                RefreshToken = refreshToken.TokenHash
            };
        }

        public async Task<IdentityResult> RegisterUser(RegisterUserDTO newUser)
        {
            User mappedUser = _mapper.Map<User>(newUser);
            var result = await _userManager.CreateAsync(mappedUser, newUser.Password!);
            await _userManager.AddToRoleAsync(mappedUser, "User");
            _logger.LogInformation("Successfully registered user: {username}, {password}", newUser.UserName, newUser.Password);
            return result;
        }
        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
