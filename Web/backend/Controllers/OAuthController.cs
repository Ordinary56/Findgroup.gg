﻿using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Findgroup_Backend.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace Findgroup_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class OAuthController(IConfiguration configuration,
        UserManager<User> manager,
        ITokenService service) : ControllerBase
    {
        
        private readonly IConfiguration _config = configuration;
        private readonly UserManager<User> _manager = manager;
        private readonly ITokenService _tokenService = service;
        [HttpPost("google-register")]
        public async Task<ActionResult> GoogleSignIn([FromBody] GoogleLoginDTO request)
        {
            if (request.Token is null || request is null) return BadRequest("Missing token");
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                IssuedAtClockTolerance = TimeSpan.FromMinutes(5),
                Audience = [_config["Google:Audience"]],
            };
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, settings);
                var user = await _manager.FindByEmailAsync(payload.Email);
                if (user is null)
                {
                    user = new User()
                    {
                        UserName = payload.FamilyName ?? payload.GivenName ?? payload.Email,
                        Email = payload.Email,
                    };
                    var result = await _manager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        var errors = string.Join(',', result.Errors);
                        return BadRequest(errors);
                    }
                }
                if (!await _manager.IsInRoleAsync(user, "User")) await _manager.AddToRoleAsync(user, "User");
                var jwtToken = await _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                Response.Cookies.Append("accessToken", jwtToken);
                Response.Cookies.Append("refreshToken", refreshToken.TokenHash);
                return Ok(new
                {
                    Message = "Succesful google login"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }

        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin()
        {
            var googleAuth = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if(!googleAuth.Succeeded)
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> AddNewAccount()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
