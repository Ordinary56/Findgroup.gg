using Findgroup_Backend.Data;
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
    public class UserController(ApplicationDbContext context, UserManager<User> manager) : ControllerBase
    {
        readonly ApplicationDbContext _context = context;
        readonly UserManager<User> _manager = manager;
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server error" + ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateNewUser([FromBody] NewUser newUser)
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
                };
                var result = await _manager.CreateAsync(createdUser, newUser.Password);
                if(result.Succeeded)
                {
                    return CreatedAtAction(nameof(GetUsers), new { Id = createdUser.Id }, newUser);
                }
                return BadRequest(result.Errors);

                
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> ModifyUser(string id, [FromBody] ModifyUserModel modifiedUser)
        {
            if (id != modifiedUser.Id) 
            {
                return BadRequest("User Id mismatch");
            }
            try
            {
                var user = await _context.Users.FindAsync(id);
                user.UserName = modifiedUser.Username;
                user.Email = modifiedUser.Email;
                user.PhoneNumber = modifiedUser.PhoneNumber;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                IdentityUser? user = await _context.Users.FindAsync(id);
                _context.Entry(user).State = EntityState.Deleted;
                await _context.Users.ExecuteDeleteAsync();
                return Ok();
            }
            catch (DBConcurrencyException)
            {
                return NotFound("Request user was not found");
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
    }

    public record NewUser(string Username, string Email, string Password);
    public record ModifyUserModel(string Id,string Username, string Email, string PhoneNumber);
}
