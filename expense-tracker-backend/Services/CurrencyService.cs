using expense_tracker_backend.Database;
using expense_tracker_backend.Domain.Domains;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker_backend.Services
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
    }
}
