using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Findgroup_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Findgroup_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly ITokenRepository _repo;
        private readonly ITokenService _service;
        private readonly UserManager<User> _manager;
        private readonly ILogger<RefreshTokenController> _logger;

        public RefreshTokenController(ITokenRepository repo, ITokenService service, UserManager<User> manager, ILogger<RefreshTokenController> logger)
        {
            _repo = repo;
            _service = service;
            _manager = manager;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async IAsyncEnumerable<RefreshToken> GetTokens()
        {
            await foreach (var token in _repo.GetAllTokens())
            {
                yield return token;
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshCurrentToken()
        {
            _logger.LogInformation("Attempting to refresh the user\'s access token");
            // Check the stored tokenhash in the cookie
            string? cookies = Request.Cookies["refreshToken"];

            // if it is null, Unauthorize
            if (cookies is null)
            {
                _logger.LogError("User\'s refresh token is null, aborting");
                return Unauthorized("Invalid refresh Token");
            }

            // Check the stored token
            RefreshToken? token = await _repo.GetStoredToken(cookies);

            // if the token is not found or the token is expired, Unauthorize
            if (token is null || token.IsRevoked) return Unauthorized("Couldn't find refresh token, either it doesn't exists or it is expired");

            // Find the target user
            User? targetUser = await _manager.FindByIdAsync(token.UserId!);
            if (targetUser is null)
            {
                _logger.LogError("Couldn\'t find user with requested Id: {userID}. Exiting...", token.UserId);
                return NotFound("User not found");
            }

            // so the user is found
            // remove the found token
            _repo.RemoveToken(token);

            // generate a new one
            var newRefreshToken = _service.GenerateRefreshToken(targetUser);

            // assign this token to the corresponsing user
            newRefreshToken.UserId = targetUser.Id;

            // Assign the token to the user
            targetUser.RefreshToken = token;

            // add the newly generated token to the database
            await _repo.AddToken(newRefreshToken);

            // prepare the cookie options
            var cookieOptions = new CookieOptions()
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7),

            };
            var Token = await _service.GenerateAccessToken(targetUser);

            // Delete the now invalid tokens in the cookie
            Response.Cookies.Delete("refreshToken");
            Response.Cookies.Delete("accessToken");

            _logger.LogInformation("Cookies have been deleted. Replacing with new ones");

            Response.Cookies.Append("refreshToken", newRefreshToken.TokenHash, cookieOptions);
            Response.Cookies.Append("accessToken", Token, cookieOptions);

            _logger.LogInformation("Successfully resumed session: {token}", newRefreshToken.TokenHash);

            // return the new Access Token
            return Ok(new
            {
                Message = "User successfully refresh it\'s token"
            });
        }

    }
}
