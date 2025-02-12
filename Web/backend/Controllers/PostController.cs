using AutoMapper;
using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController(IPostRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly IPostRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
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
        public async Task<ActionResult> CreateNewPost([FromBody] PostDTO postDTO)
        {
            try
            {
                Post post = _mapper.Map<Post>(postDTO);
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
        public async Task<ActionResult> ModifyPost([FromBody] PostDTO content)
        {
            try
            {
                Post post = _mapper.Map<Post>(content);
                await _repository.ModifyPostAsync(post);
                return NoContent();
            }
            catch (DBConcurrencyException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server error: " + ex.Message);
            }
        }
    }

}
