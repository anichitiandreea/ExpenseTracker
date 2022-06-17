using ExpenseTracker.Database;
using ExpenseTracker.Domain.Domains;
using ExpenseTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly DatabaseContext context;

        public CurrencyService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Currency>> GetAllAsync()
        {
            return await context.Currencies.ToListAsync();
        }

        public async Task<Currency> GetByIdAsync(Guid id)
        {
            return await context.Currencies
                .FirstOrDefaultAsync(currency => currency.Id == id);
        }

        public async Task CreateAsync(Currency category)
        {
            await context.Currencies.AddAsync(category);
            await context.SaveChangesAsync();
        }
    }
}
