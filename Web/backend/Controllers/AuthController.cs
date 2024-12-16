using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Findgroup_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<User> userManager,
    IConfiguration configuration,
    SignInManager<User> signInManager,
    ILogger<AuthController> logger
    ) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly ILogger<AuthController> _logger = logger;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, true);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to authenticate user (using: {Username} - {Password})", model.Username, model.Password);
            return Unauthorized("Invalid Login attempt");
        }
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            _logger.LogError("User is null");
            return Unauthorized();
        }

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, "User")
        };

        var token = GenerateAccessToken(authClaims);
        _logger.LogInformation("Successfully Authenticated user");
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }
    private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));

        return new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
    }


}

// DTOs
public sealed record LoginModel
{
    public string Username { get; init; }
    public string Password { get; init; }
}
