using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly UserManager<User> _manager;

        // TODO: this is the endpoint that should be called when uploading the activity
        [HttpPost("upload")]
        public async Task<IActionResult> UploadCurrentActivity([FromQuery] string userId, [FromBody])
        {
            
        }

        [HttpPatch("set")]
        public async Task<IActionResult> SetCurrentActivity()
        {
            throw new NotImplementedException();
        }
    }
}
