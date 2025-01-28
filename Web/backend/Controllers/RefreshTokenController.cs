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
    public class RefreshTokenController(IConfiguration configuration, UserManager<User> manager) : ControllerBase, ITokenHandler
    {
        private readonly IConfiguration _configuration = configuration;
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
            var newAccessToken = ITokenHandler.GenerateAccessToken(principal.Claims,_configuration);
            var newRefreshToken = ITokenHandler.GenerateRefreshToken();
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
            var validationParams = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, validationParams, out var securityToken);
            if(securityToken is JwtSecurityToken jwtSecurityToken && 
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
            throw new SecurityTokenException("Invalid Token");
        }
    }
    public sealed record RefreshTokenModel(string Token);
}
