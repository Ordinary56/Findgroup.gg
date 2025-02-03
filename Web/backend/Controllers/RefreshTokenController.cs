using Findgroup_Backend.Helpers;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
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
    public class RefreshTokenController( UserManager<User> manager) : ControllerBase
    {
        private readonly UserManager<User> _manager = manager;
        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDTO model)
        {
            if(User.Identity.IsAuthenticated)
            {
                // Check if user's refresh token is still valid
                // if yes -> return either return badrequest or throw error
                // if no -> generate a new one
            }
            await Task.Delay(1);
            throw new NotImplementedException();
        }
        
    }
}
