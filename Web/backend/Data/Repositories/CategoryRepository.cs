using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository, IDisposable
    {
        private readonly ApplicationDbContext _context = context;
        private bool _disposed = false;

        public async Task CreateNewCategory(Category newCategory)
        {
            await _context.Categories.AddAsync(newCategory);
        }

        public async Task DeleteCategory(int id)
        {
            Category target = await _context.Categories.FindAsync(id) ?? throw new Exception();
            _context.Categories.Remove(target);

        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IAsyncEnumerable<Category> GetCategories()
        {
            return _context.Categories.AsAsyncEnumerable();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
           
            return await _context.Categories.FindAsync(id) ?? throw new Exception("Couldn't find requested category");
        }

        public Task ModifyCategory(Category modifiedCategory)
        {
            throw new NotImplementedException();
        }

        protected void Dispose(bool disposing) 
        {
            if (disposing)
            {
                _context.Dispose(); 
            }
            _disposed = true;
        }
    }
}
