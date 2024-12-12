using Findgroup_Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Findgroup_Backend.Controllers
{
    public class UserController : ControllerBase
    {
        ApplicationDbContext _context;
       public ActionResult GetUsers()
       {
            return Ok();
       }
    }
}
