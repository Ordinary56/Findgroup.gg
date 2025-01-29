using Findgroup_Backend.Data;
using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findgroup_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryRepository repository) : ControllerBase
    {
        private readonly ICategoryRepository _repository = repository;

        [Authorize]
        [HttpGet]
        public IAsyncEnumerable<Category> GetCategories()
        {
            return _repository.GetCategories();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateNewCategory([FromBody] NewCategoryModel newCategory)
        {
            
            Category newOne = new()
            {
                CategoryName = newCategory.CategoryName
            };
            await _repository.CreateNewCategory(newOne);
            return CreatedAtAction(nameof(GetCategories), new { Id = newOne.Id }, newOne);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await Task.Delay(100);
            throw new NotImplementedException();
        }

        // DTOs
        public sealed record NewCategoryModel
        {
            public string CategoryName { get; init; } = "";
        }
    }
}
