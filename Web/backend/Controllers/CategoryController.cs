using AutoMapper;
using Findgroup_Backend.Data;
using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findgroup_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly ICategoryRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        [Authorize]
        [HttpGet]
        public async IAsyncEnumerable<Category> GetCategories()
        {
            await foreach (Category category in _repository.GetCategories())
            {
                yield return category;
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateNewCategory([FromBody] CategoryDTO newCategory)
        {
            Category newOne = _mapper.Map<Category>(newCategory);
            await _repository.CreateNewCategory(newOne);
            return CreatedAtAction(nameof(GetCategories), new { Id = newOne.Id }, newOne);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _repository.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> ModifyCategory([FromBody] CategoryDTO modified)
        {
            try
            {
                Category target = _mapper.Map<Category>(modified);
                await _repository.ModifyCategory(target);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

    }
}
