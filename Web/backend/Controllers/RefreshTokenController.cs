using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Helpers;
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
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDTO model)
        {
            // Check the stored token
            RefreshToken? token = await _repo.GetStoredToken(model.Token);

            // if the token is not found or the token is expired, Unauthorize
            if(token is null || _service.IsTokenExpired(token)) return Unauthorized();

            // Find the target user
            User? targetUser = await _manager.FindByIdAsync(token.UserId!);
            if(targetUser is null) return NotFound("User not found");

            // return the new Access Token
            return Ok(new
            {
                Token = await _service.GenerateAccessToken(targetUser)
            });
        }
        
    }
}
