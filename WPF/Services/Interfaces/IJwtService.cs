using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Services.Interfaces
{
    public interface IJwtService
    {
        public ClaimsPrincipal DecodeToken(string token);
    }
}
