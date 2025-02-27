using AutoMapper;
using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
namespace Findgroup_Backend.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public sealed class MessageController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageRepository _repository;
        private readonly Mapper _mapper;
        private readonly IGroupRepository _groupRepo;


        public MessageController(IConfiguration config, IMessageRepository repo, Mapper mapper, IGroupRepository groupRepo)
        {
            _configuration = config;
            _repository = repo;
            _mapper = mapper;
            _groupRepo = groupRepo;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async IAsyncEnumerable<Message> GetAllMessages()
        {
            await foreach(var message in _repository.GetMessages())
            {
                yield return message;
            }
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async IAsyncEnumerable<Message?> GetGroupMessages([FromQuery] string groupId)
        {
            if (!Guid.TryParse(groupId, out var id))
            {
                await Task.CompletedTask;
                yield break;
            }
            await foreach(Message message in _repository.GetMessages().Where(x => x.GroupId == id))
            {
                yield return message;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO dto)
        {
            Message message = _mapper.Map<Message>(dto);
            Group? target = await _groupRepo.GetGroupById(message.GroupId);
            if (target == null) return BadRequest(new
            {
                Message = $"Failed to send message to group (Id = {message.GroupId})"
            });

            await _repository.AddNewMessage(message);
            target.Messages.Add(message);
            return CreatedAtAction(nameof(GetAllMessages), new {Id = message.Id}, dto);
        }
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> ModifyMessage([FromBody] MessageDTO message)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteMessage([FromBody] MessageDTO message)
        {

            if (!Guid.TryParse(message.GroupId, out Guid id)) return BadRequest(new
            {
                Message = "Failed to delete message"
            });
            Group? group = await _groupRepo.GetGroupById(id);
            if (group == null) return BadRequest(new
            {
                Message = "Group does not exist"
            });
            Message? messageToBeRemoved = group.Messages.FirstOrDefault(x => x.Id == message.Id);
            group.Messages.Remove(messageToBeRemoved);
            await _repository.RemoveMessage(messageToBeRemoved.Id);
            return NoContent();
        }

    }
}
