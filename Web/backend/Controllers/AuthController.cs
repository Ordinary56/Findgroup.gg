using Findgroup_Backend.Helpers;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace Findgroup_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<User> userManager,
    IConfiguration configuration,
    SignInManager<User> signInManager,
    ILogger<AuthController> logger
    ) : ControllerBase, ITokenHandler
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

        // Generate Authentication Token for user
        var token = ITokenHandler.GenerateAccessToken(authClaims, _configuration);
        // Generate Refresh Token for user
        var refreshToken = ITokenHandler.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        // Expiry time is 1 week
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        // Update the user
        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Successfully Authenticated user");
        // return the tokens
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo,
            refreshToken
        });
    }
    [HttpPost("register")]
    public async Task<ActionResult> RegisterNewUser([FromBody] RegisterModel newUser)
    {
        if (newUser == null)
        {
            return BadRequest("User is null");
        }
        try
        {
            User createdUser = new()
            {
                UserName = newUser.Username,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(createdUser, newUser.Password);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(UserController.GetUsers), new { Id = createdUser.Id }, newUser);
            }
            return BadRequest(result.Errors);


        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error" + ex.Message);
        }
    }
    [Authorize(Roles = "User, Admin")]
    [HttpPost("logout")] 
    public async Task<ActionResult> LogoutUser()
    {
        try
        {
            if (!User.Identity.IsAuthenticated)
            {
                _logger.LogError("{currentUser}", User);
                return Unauthorized("Unauthorized");
            }
            await _signInManager.SignOutAsync();
            return Ok();
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }

    }
 

}

// DTOs
public record LoginModel
{
    public string Username { get; init; } = "";
    public string Password { get; init; } = "";
}

public sealed record RegisterModel : LoginModel
{
    [Required]
    public string Email { get; init; } = "";
    public string PhoneNumber { get; init; } = "";
}
