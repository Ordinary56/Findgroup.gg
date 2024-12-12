using Findgroup_Backend.Data;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController(ApplicationDbContext context, ILogger<UserController> logger) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<UserController> _logger = logger;
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsersAsync()
        {

            try
            {
                var users = await _context.Users.ToListAsync();
                _logger.LogInformation("{Users}", string.Join(", ", users));
                return Ok(JsonSerializer.Serialize<List<IdentityUser>>(users));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to fetch users: {ErrorMessage}\n Status code: {Code}", ex.Message, 500);
                return StatusCode(500,"Internal Server erorr: "+ ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateNewUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                _logger.LogError("Failed to create new user!\n Revieced from body : {Content}\n", newUser);
                return BadRequest("Failed to create new User (user is null)!");
            }
            try
            {
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                _logger.LogInformation("New user created successfully: {newUser}", newUser);
                return CreatedAtAction(nameof(GetUsersAsync), new {Id = newUser.Id},newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create new User: {ErrorMessage}\n Status code: {Code}", ex.Message, 500);
                return StatusCode(500,"Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(string id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id) 
            {
                return BadRequest("User Id Mismatch");
            }
            try
            {
                _context.Entry(updatedUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DBConcurrencyException)
            {
                return NotFound("User not found");
            }
            catch (Exception e) 
            {
                return StatusCode(500, "Internal Server Error: " + e.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id, [FromBody] User deletedUser)
        {
            await Task.Delay(1000);
            return NoContent();
        }

    }
}
