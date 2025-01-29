using AutoMapper;
using Findgroup_Backend.Data;
using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Policy;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController(IUserRepository repository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = repository;
        [HttpGet]
        // [Authorize(Roles = "Admin")]
        public async IAsyncEnumerable<User> GetUsers()
        {
            await foreach( User user in _userRepository.GetUsers())
            {
                yield return user;
            }
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> ModifyUser([FromRoute] string id, [FromBody] ModifyUserModel modifiedUser)
        {
            if (id != modifiedUser.Id) 
            {
                return BadRequest("User Id mismatch");
            }
            try
            {
                Mapper mapper = new(null);
                User _mappedDTO = mapper.Map<User>(modifiedUser);
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
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }

    public record NewUser(string Username, string Email, string Password);
    public record ModifyUserModel(string Id,string Username, string Email, string PhoneNumber);
}
