using ExpenseTracker.Database;
using ExpenseTracker.Domain;
using ExpenseTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext context;

        public CategoryService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await context.Categories
                .FirstOrDefaultAsync(category =>
                    category.Id == id
                    && !category.IsDeleted);
        }

        public async Task CreateAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
