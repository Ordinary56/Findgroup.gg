using AutoMapper;
using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public async IAsyncEnumerable<UserDTO> GetUsers()
        {
            await foreach (UserDTO user in _userRepository.GetUsers())
            {
                yield return user;
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("me")]
        public IActionResult GetUserInfo()
        {
            var claims = User.Claims.ToDictionary(c => c.Type.Split('/')[^1], c => c.Value);
            return Ok(claims);
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
