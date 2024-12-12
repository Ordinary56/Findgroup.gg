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
    public class UserController(ApplicationDbContext context) : ControllerBase
    {
        ApplicationDbContext _context = context;
        [HttpGet]
        public async Task<ActionResult<List<IdentityUser>>> GetUsers()
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
        public async Task<ActionResult> CreateNewUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("User is null");
            }
            try
            {
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUsers), new { newUser.Id}, newUser);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> ModifyUser(string id, [FromBody] User modifiedUser)
        {
            if (id != modifiedUser.Id) 
            {
                return BadRequest("User Id mismatch");
            }
            try
            {
                _context.Entry(modifiedUser).State = EntityState.Modified;
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
                _context.Entry<IdentityUser>(user).State = EntityState.Deleted;
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
}
