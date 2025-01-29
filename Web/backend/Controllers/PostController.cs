using AutoMapper;
using Findgroup_Backend.Data;
using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController(IPostRepository repository) : ControllerBase
    {
        private readonly IPostRepository _repository = repository;
        [HttpGet]
        [Authorize(Roles = "User")]
        public async IAsyncEnumerable<Post> GetPosts()
        { 
                await foreach (var post in _repository.GetPosts()) 
                {
                    yield return post;
                }
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetPost(int id)
        {
            try
            {
                Post post = await _repository.GetPostById(id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server error: " + ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateNewPost([FromBody] PostModel postDTO)
        {
            Post post = new()
            {
                Content = postDTO.Content,
                UserId = postDTO.UserId
            };
            try
            {
                await _repository.CreateNewPost(post);
                return CreatedAtAction(nameof(GetPosts), new { Id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }

        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult> ModifyPost([FromBody] ModifyPostModel content)
        {
            try
            {
                Mapper mapper = new(null);
                Post post = mapper.Map<Post>(content);
                await _repository.ModifyPostAsync(post);
                return NoContent();
            }
            catch(DBConcurrencyException)
            {
                return NotFound();
            }
        }
        public record UserModel(string Id);
        public record PostModel(string Content, string UserId);
        public record ModifyPostModel(string Content, int Id);
    }

}
