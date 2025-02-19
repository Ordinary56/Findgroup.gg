using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Findgroup_Backend.Helpers
{
    public interface IJwtDecoder
    {
        public static (bool isValid, Dictionary<string, string> claims) DecryptToken(string token);
    }
    public class JwtDecoder : IJwtDecoder
    {
        private readonly IConfiguration _configuration;

        public static (bool isValid, Dictionary<string, string> claims) DecryptToken(string token)
        {

            Dictionary<string, string> claims = [];

            var validationParams = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey()

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            foreach (var claim in jwtToken.Claims) 
            {
                claims[claim.Type] = claim.Value;
            }
            return (true, claims);
        }
    }
}
