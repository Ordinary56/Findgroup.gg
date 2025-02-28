using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WPF.Services.Interfaces;

namespace WPF.Services
{
    public class JwtService : IJwtService
    {
        JwtSecurityTokenHandler _handler;
        public JwtService()
        {
            _handler = new JwtSecurityTokenHandler();   
        }
        public ClaimsPrincipal DecodeToken(string token)
        {
            var decoded = _handler.ReadJwtToken(token);
            return new ClaimsPrincipal(new ClaimsIdentity(decoded.Claims));
        }
    }
}
