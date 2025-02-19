﻿using AutoMapper;
using Findgroup_Backend.Data;
using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Policy;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController(IUserRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly IUserRepository _userRepository = repository;
        private readonly IMapper _mapper = mapper;
        [HttpGet]
        // [Authorize(Roles = "Admin")]
        public async IAsyncEnumerable<User> GetUsers()
        {
            await foreach (User user in _userRepository.GetUsers())
            {
                yield return user;
            }
        }
        
        [HttpGet("me")]
        public async Task<IActionResult> GetUserInfo()
        {
            var accessToken = Request.Cookies["accessToken"];
            if (string.IsNullOrEmpty(accessToken)) return Unauthorized(new
            {
                Message = "No access token provided"
            });
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            User found = await _userRepository.GetUserById(id);
            if (found != null) return Ok(found);
            return NotFound();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> ModifyUser([FromRoute] string id, [FromBody] UserDTO modifiedUser)
        {
            if (id != modifiedUser.Id)
            {
                return BadRequest("User Id mismatch");
            }
            try
            {
                User _mappedDTO = _mapper.Map<User>(modifiedUser);
                await _userRepository.UpdateUser(_mappedDTO);
                return NoContent();
            }
            catch (DBConcurrencyException)
            {
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
                return Ok();
            }
            catch (DBConcurrencyException)
            {
                return NotFound("Request user was not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }

}
