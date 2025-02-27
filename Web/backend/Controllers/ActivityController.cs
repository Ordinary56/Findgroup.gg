using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Input;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly UserManager<User> _manager;

        public ActivityController(UserManager<User> manager)
        {
            _manager = manager;
        }

        // TODO: this is the endpoint that should be called when uploading the activity
        [HttpPost("upload")]
        public async Task<IActionResult> UploadCurrentActivity([FromQuery] string userId, [FromBody] List<ActivityDTO> processes)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("set")]
        public async Task<IActionResult> SetCurrentActivity()
        {
            throw new NotImplementedException();
        }
    }
}
