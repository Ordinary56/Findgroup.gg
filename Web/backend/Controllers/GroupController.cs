using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class GroupController(IGroupRepository groupRepo, IUserRepository userRepo) : ControllerBase
    {
        private readonly IGroupRepository _groupRepository = groupRepo;
        private readonly IUserRepository _userRepository = userRepo;

        [HttpGet]
        public async IAsyncEnumerable<Group> GetGroups()
        {
            await foreach (Group group in _groupRepository.GetGroups())
            {
                yield return group;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroupById(string id)
        {
            if (!Guid.TryParse(id, out Guid targetGuid))
            {
                return BadRequest("Invalid Guid for Group");
            }
            Group? target = await _groupRepository.GetGroupById(targetGuid);
            if (target == null) return NotFound("Requested user not found");
            return Ok(target);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateNewGroup([FromQuery] string name, [FromQuery] string description, [FromQuery] int memberLimit, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("Invalid parameters for name and userId");
            }
            if (memberLimit <= 0)
            {
                return BadRequest("memberLimit can't be 0 or negative");
            }
            User creator = await _userRepository.GetUserById(userId);
            await _groupRepository.CreateNewGroup(name, description, memberLimit, creator);
            return StatusCode(201, new
            {
                Message = "New group successfully created!",
                GroupName = name,
                MemberLimit = memberLimit
            });
        }

        [HttpPost("/join")]
        public async Task<ActionResult> JoinGroup([FromQuery] Guid groupId, [FromQuery] string userId)
        {
            if (groupId == Guid.Empty || string.IsNullOrEmpty(userId))
            {
                return BadRequest(new
                {
                    Message = "Invalid group or user Id",
                    GroupId = groupId,
                    UserId = userId
                });
            }
            try
            {
                User newMember = await _userRepository.GetUserById(userId);
                Group? group = await _groupRepository.GetGroupById(groupId);
                if (group is null || newMember is null) return BadRequest("Invalid request");
                await _groupRepository.JoinGroup(group, newMember);
                return Ok($"User: {newMember.UserName} successfully joined to group (Id) : {group.Id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("leave")]
        public async Task<ActionResult> LeaveGroup([FromQuery] Guid groupId, string userId)
        {
            Group? target = await _groupRepository.GetGroupById(groupId);
            User? user = await _userRepository.GetUserById(userId);
            if (user is null || target is null) return BadRequest("Invalid request");
            await _groupRepository.LeaveGroup(target, user);
            return Ok(new
            {
                Message = "Successfully left group!"
            });
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteGroup(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name must not be empty");
            }
            try
            {
                await _groupRepository.DeleteGroup(name);
                return Ok($"Successfully deleted Group with name: {name}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
