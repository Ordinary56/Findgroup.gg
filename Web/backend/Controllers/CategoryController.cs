using Findgroup_Backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findgroup_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized($"Please login to see this endpoint ({nameof(GetCategories)})");
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal Server Error: ", ex.Message);
            }
        }
    }
}
