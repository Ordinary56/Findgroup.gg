using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Findgroup_Backend.Helpers;
public interface ITokenHandler
{
    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims);
    public string GenerateRefreshToken();
}
public interface IOAuthTokenEncryptor
{
    public string EncryptToken(string token);
}


/// <summary>
/// TODO: Implement token encryption for OAuth tokens
/// </summary>
public class OAuthTokenEncryptor : IOAuthTokenEncryptor
{
    public string EncryptToken(string token)
    {
        throw new NotImplementedException();
    }
}

public class TokenHandler(IConfiguration configuration) : ITokenHandler
{
    private readonly IConfiguration _configuration = configuration;
    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secret = Environment.GetEnvironmentVariable("JWT_SECRET") ??
            throw new Exception("JWT key not found");

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        return new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
    }
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        Rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

}


