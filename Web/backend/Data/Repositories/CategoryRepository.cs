﻿using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Findgroup_Backend.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;
        private bool _disposed = false;

        public CategoryRepository(ApplicationDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateNewCategory(Category newCategory)
        {
            if (await _context.Categories.AnyAsync(c => c.CategoryName == newCategory.CategoryName))
            {
                throw new InvalidOperationException("This category already exists!");
            }
            await _context.Categories.AddAsync(newCategory);
        }

        public async Task DeleteCategory(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            Category target = await _context.Categories.FindAsync(id) ??
                throw new KeyNotFoundException($"Category with id={id} not found");
            _context.Categories.Remove(target);
        }


        public IAsyncEnumerable<Category> GetCategories()
        {
            return _context.Categories.AsAsyncEnumerable();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            _logger.LogInformation("Attempting to find category with requested ID: {Id}", id);
            Category? target = await _context.Categories.FindAsync(id);
            return target;
        }

        public async Task ModifyCategory(Category modifiedCategory)
        {
            Category target = await _context.Categories.FindAsync(modifiedCategory.Id) ?? throw new Exception();
            _context.Categories.Remove(target);
            await _context.Categories.AddAsync(modifiedCategory);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
