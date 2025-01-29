using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories
{
    public interface ICategoryRepository : IDisposable
    {
        public IAsyncEnumerable<Category> GetCategories();
        public Task<Category> GetCategoryById(int id);
        public Task CreateNewCategory(Category newCategory);
        public Task ModifyCategory(Category modifiedCategory);
        public Task DeleteCategory(int id);
        public Task Save();
    }
}
