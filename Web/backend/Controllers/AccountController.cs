using Findgroup_Backend.Models;
using Findgroup_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Findgroup_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;

        }

        [HttpGet("login-google")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return BadRequest("Error loading external login info");
            }
            string? email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            string? name = loginInfo.Principal.FindFirstValue(ClaimTypes.Name);
            User? user = await _userManager.FindByEmailAsync(email!);
            if (user == null)
            {
                user = new() { UserName = name, Email = email, };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest("Failed to create user");
                }
                var addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
                if (!addLoginResult.Succeeded)
                {
                    return BadRequest("Failed to add external login info!");
                }
            }
            else
            {
                var existingLogins = await _userManager.GetLoginsAsync(user);
                if (!existingLogins.Any(x => x.LoginProvider == loginInfo.LoginProvider))
                {
                    await _userManager.AddLoginAsync(user, loginInfo);
                }
            }
            var token = await _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user);
            user.RefreshToken = refreshToken;
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(1)
            };
            Response.Cookies.Append("accessToken", token, cookieOptions);
            Response.Cookies.Append("refreshToken", refreshToken.TokenHash, cookieOptions);
            return Ok(new
            {
                Message = "User Successfully logged in via Google!"
            });
        }
    }
}
