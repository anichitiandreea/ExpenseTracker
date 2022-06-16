using ExpenseTracker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetAllAsync();
        Task<Currency> GetByIdAsync(Guid id);
        Task CreateAsync(Currency category);
    }
}
