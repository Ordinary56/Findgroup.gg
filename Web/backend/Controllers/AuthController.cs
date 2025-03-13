using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models.DTOs.Input;
using Findgroup_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace Findgroup_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly IConfiguration _config;
    private readonly ITokenRepository _tokenRepo;

    public AuthController(IAuthService auth, IConfiguration config, ITokenRepository tokenRepo)
    {
        _auth = auth;
        _config = config;
        _tokenRepo = tokenRepo;

    }

    [HttpGet("validate-token")]
    public async Task<IActionResult> ValidateToken()
    {
        string token = Request.Cookies["accessToken"]!;
        string key = _config["JwtSecret"]!;
        if (string.IsNullOrEmpty(token)) return Unauthorized(new
        {
            Valid = false
        });
        TokenValidationParameters validationParams = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _config["JwtSettings:Issuer"],
            ValidAudience = _config["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var result = await tokenHandler.ValidateTokenAsync(token, validationParams);
        return Ok(new
        {
            Valid = result.IsValid
        });

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        try
        {
            var result = await _auth.LoginUser(model);
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                Secure = true,
                SameSite = SameSiteMode.Lax,
            };
            Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);
            Response.Cookies.Append("accessToken", result.Token, cookieOptions);
            return Ok(new
            {
                message = "Login successfull"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPost("register")]
    public async Task<ActionResult> RegisterNewUser([FromBody] RegisterUserDTO newUser)
    {
        IdentityResult result = await _auth.RegisterUser(newUser);
        if (result.Succeeded) return StatusCode(201, new { Message = "New User successfully created!", user = newUser });
        return StatusCode(500, "Internal Server error: " + result.Errors);
    }

    [Authorize(Roles = "User, Admin")]
    [HttpPost("logout")]
    public async Task<ActionResult> LogoutUser()
    {
        try
        {
            await _auth.LogoutUser();
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");
            return Ok(new
            {
                Message = "Successfully logged out user"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


}
