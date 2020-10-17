using expense_tracker_backend.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expense_tracker_backend.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task CreateAsync(Category category);
    }
}
