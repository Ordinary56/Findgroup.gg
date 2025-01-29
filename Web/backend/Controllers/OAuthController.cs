using Microsoft.AspNetCore.Mvc;

namespace Findgroup_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class OAuthController : ControllerBase
    {
        // TODO: implement OAuth controller

        [HttpPatch]
        public async Task<ActionResult> AddNewAccount()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
