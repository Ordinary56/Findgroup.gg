using AutoMapper;
using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Input;
using Findgroup_Backend.Models.DTOs.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Findgroup_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostController> _logger;
        private readonly IUserRepository _userRepo;
        private readonly ICategoryRepository _categoryRepo;

        public PostController(IPostRepository repository, IMapper mapper,
                ILogger<PostController> logger,
                IUserRepository userRepo,
                ICategoryRepository categoryRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userRepo = userRepo;
            _categoryRepo = categoryRepo;
        }
        [HttpGet]
        [Authorize()]
        public async IAsyncEnumerable<PostDTO> GetPosts()
        {
            await foreach (var post in _repository.GetPosts())
            {
                yield return post;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            try
            {
                PostDTO? post = await _repository.GetPostById(id);
                return post == null ? NotFound() : Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server error: " + ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateNewPost([FromBody] CreatePostDTO postDTO)
        {
            try
            {
                Post post = _mapper.Map<Post>(postDTO);
                _logger.LogInformation("Mapper return post with {Post}", post);
                User creator = await _userRepo.GetUserById(postDTO.UserId);
                Category category = await _categoryRepo.GetCategoryById(postDTO.CategoryId);
                post.UserId = creator.Id;
                post.CategoryId = category.Id;
                await _repository.CreateNewPost(post, creator, category);
                return CreatedAtAction(nameof(GetPost), new { Id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }

        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult> ModifyPost([FromBody] CreatePostDTO content)
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
