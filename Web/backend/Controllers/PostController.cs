using Findgroup_Backend.Data;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
    private record UserModel();
}
