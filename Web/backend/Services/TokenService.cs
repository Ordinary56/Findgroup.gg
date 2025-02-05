
using Findgroup_Backend.Data;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Findgroup_Backend.Services
{
    public sealed class TokenService(IConfiguration configuration, UserManager<User> manager) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<User> _manager = manager;

        

        private static string HashToken(string token)
        {
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var hashBytes = SHA256.HashData(tokenBytes);
            return Convert.ToBase64String(hashBytes);
        }

        /// <inheritdoc/>
        public async Task<string> GenerateAccessToken(User user)
        {
            string jwtKey = _configuration["JwtSecret"]!;
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtKey));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
            List<Claim> authClaims = [
                new(ClaimTypes.Name, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id)
            ];
            var roles = await _manager.GetRolesAsync(user);
            foreach (string role in roles) authClaims.Add(new(ClaimTypes.Role, role));
            IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");
            JwtSecurityToken token = new(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: authClaims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        /// <inheritdoc/>
        public RefreshToken GenerateRefreshToken()
        {
            byte[] randomBytes = new byte[64];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            string token = Convert.ToBase64String(randomBytes);
            return new RefreshToken()
            {
                TokenHash = HashToken(token),
                ExpiresOnUTC = DateTime.UtcNow.AddDays(7)
            };
        }

    }
}
