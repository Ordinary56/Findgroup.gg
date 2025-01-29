using Findgroup_Backend.Helpers;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Findgroup_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController(ITokenHandler handler, UserManager<User> manager) : ControllerBase
    {
        private readonly ITokenHandler _tokenHandler = handler;
        private readonly UserManager<User> _manager = manager;
        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            var principal = GetPrincipalFromExpiredToken(model.Token);
            if (principal == null) return BadRequest("Invalid access or refresh token");
            string username = principal.Identity!.Name!;
            User? user = await _manager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != model.Token || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return BadRequest("Invalid Refresh Token");
            var newAccessToken = _tokenHandler.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenHandler.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _manager.UpdateAsync(user);
            return Ok(new
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            string secret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new Exception("JWT secret not found");
            var validationParams = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, validationParams, out var securityToken);
            if (securityToken is JwtSecurityToken jwtSecurityToken &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
            throw new SecurityTokenException("Invalid Token");
        }
    }
    public sealed record RefreshTokenModel(string Token);
}
