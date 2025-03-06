using Findgroup_Backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Findgroup_Backend.Controllers
{
    [Obsolete("this class was used for testing purposes. All the authentication works properly")]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(ApplicationDbContext context) : ControllerBase
    {
        readonly ApplicationDbContext _context = context;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Home()
        {
            await Task.Delay(1);
            return Ok("Bennt vagy");
        }
    }
}
