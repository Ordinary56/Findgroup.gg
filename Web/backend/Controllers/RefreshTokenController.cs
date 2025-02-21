using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Findgroup_Backend.Services;
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
    public class RefreshTokenController(ITokenRepository repo,
        ITokenService service, UserManager<User> manager) : ControllerBase
    {
        private readonly ITokenRepository _repo = repo;
        private readonly ITokenService _service = service;
        private readonly UserManager<User> _manager = manager;

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
        public async Task<ActionResult> RefreshToken()
        {
            // Check the stored tokenhash in the cookie
            string? cookies = Request.Cookies["refreshToken"];

            // if it is null, Unauthorize
            if (cookies is null) return Unauthorized("Invalid refresh Token");

            // Check the stored token
            RefreshToken? token = await _repo.GetStoredToken(cookies);

            // if the token is not found or the token is expired, Unauthorize
            if (token is null || token.IsRevoked) return Unauthorized("Couldn't find refresh token, either it doesn't exists or it is expired");

            // Find the target user
            User? targetUser = await _manager.FindByIdAsync(token.UserId!);
            if (targetUser is null) return NotFound("User not found");

            // so the user is found
            // remove the found token
            _repo.RemoveToken(token);

            // generate a new one
            var newRefreshToken = _service.GenerateRefreshToken();

            // assign this token to the corresponsing user
            newRefreshToken.UserId = targetUser.Id;

            // add the newly generated token to the database
            await _repo.AddToken(newRefreshToken);

            targetUser.RefreshToken = token;
            // prepare the cookie options
            var cookieOptions = new CookieOptions()
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7),

            };

            Response.Cookies.Append("refreshToken", newRefreshToken.TokenHash, cookieOptions);

            // return the new Access Token
            return Ok(new
            {
                Token = await _service.GenerateAccessToken(targetUser)
            });
        }

    }
}
