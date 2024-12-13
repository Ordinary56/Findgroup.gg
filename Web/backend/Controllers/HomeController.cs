using Findgroup_Backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Findgroup_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(ApplicationDbContext context) : ControllerBase
    {
        ApplicationDbContext _context = context;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Home()
        {
            await Task.Delay(1);
            return Ok("Bennt vagy");
        }
    }
}
