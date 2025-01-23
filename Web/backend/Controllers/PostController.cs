using Findgroup_Backend.Data;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        [HttpGet]
        public async Task<ActionResult> GetPosts()
        {
            try
            {
                List<Post> posts = await _context.Posts.ToListAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetPost(int id)
        {
            try
            {
                Post post = await _context.Posts.SingleAsync(p => p.Id == id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server error: " + ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateNewPost([FromBody] PostModel postDTO)
        {
            Post post = new()
            {
                Content = postDTO.Content,
                UserId = postDTO.UserId
            };
            try
            {
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPosts), new { Id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }

        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult> ModifyPost([FromBody] ModifyPostModel content)
        {
            try
            {
                var target = await _context.Posts.FindAsync(content.Id);
                target.Content = content.Content;
                _context.Entry(target).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch(DBConcurrencyException)
            {
                return NotFound();
            }
        }
        public record UserModel(string Id);
        public record PostModel(string Content, string UserId);
        public record ModifyPostModel(string Content, int Id);
    }

}
