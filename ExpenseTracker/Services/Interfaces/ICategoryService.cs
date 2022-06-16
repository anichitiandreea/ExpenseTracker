using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
