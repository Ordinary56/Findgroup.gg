using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Input;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult> CreateNewGroup([FromBody] CreateGroupDTO dto)
        {
            if (string.IsNullOrEmpty(dto.GroupName) || string.IsNullOrEmpty(dto.UserId))
            {
                return BadRequest("Invalid parameters for name and userId");
            }
            if (dto.MemberLimit <= 0)
            {
                return BadRequest("memberLimit can't be 0 or negative");
            }
            User creator = await _userRepository.GetUserById(dto.UserId);
            await _groupRepository.CreateNewGroup(dto, creator);
            return StatusCode(201, new
            {
                Message = "New group successfully created!",
                dto.GroupName,
                dto.MemberLimit
            });
        }
        [HttpPost("join"), Authorize]
        public async Task<ActionResult> JoinGroup([FromQuery] string groupId, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(groupId))
            {
                return BadRequest(new
                {
                    Message = "Invalid group or user Id",
                    GroupId = groupId,
                    UserId = userId
                });
            }
            if(!Guid.TryParse(groupId, out Guid result))
            {
                return BadRequest(new
                {
                    Message = "Invalid GroupId"
                });
            }
            try
            {
                User newMember = await _userRepository.GetUserById(userId);
                Group? group = await _groupRepository.GetGroupById(result);
                if (group is null || newMember is null) return BadRequest("Invalid request");
                await _groupRepository.JoinGroup(group, newMember);
                return Ok($"User: {newMember.UserName} successfully joined to group (Id) : {group.Id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("leave"), Authorize]
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

        [HttpDelete("{name}"), Authorize]
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
