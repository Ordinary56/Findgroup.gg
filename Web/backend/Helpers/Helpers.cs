// TODO: use helper methods later
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Findgroup_Backend.Helpers;
public interface ITokenHandler
{
    public static JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));

        return new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
    }
    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        Rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);

    }
}


/// <summary>
/// TODO: Implement token encryption for OAuth tokens
/// </summary>
public static class OAuthTokenEncryptor
{

    public static string EncryptToken(string token)
    {
        throw new NotImplementedException();
    }
}
