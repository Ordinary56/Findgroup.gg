using Findgroup_Backend.Models.DTOs;
using Findgroup_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Findgroup_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _auth = authService;

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
                SameSite = SameSiteMode.Strict,
            };
            Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);
            Response.Cookies.Append("accessToken", result.Token, cookieOptions with
            {
                Expires = DateTime.UtcNow.AddMinutes(15),
            });
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
    public async Task<ActionResult> RegisterNewUser([FromBody] UserDTO newUser)
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
