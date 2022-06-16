using ExpenseTracker.Database;
using ExpenseTracker.Domain;
using ExpenseTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly DatabaseContext context;

        public StatisticsService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Category>> GetAllCategoriesWithTransactionsAsync()
        {
            return await context.Categories
                .Include(c => c.Transactions)
                .ToListAsync();
        }
    }
}
