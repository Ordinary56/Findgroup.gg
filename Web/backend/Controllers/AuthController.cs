﻿using AutoMapper;
using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Helpers;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Findgroup_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            return Ok(result);
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
        if (result.Succeeded) return StatusCode(201, new {Message = "New User successfully created!", user = newUser});
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
