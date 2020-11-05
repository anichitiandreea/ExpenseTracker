using expense_tracker_backend.Database;
using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker_backend.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DatabaseContext context;

        public TransactionService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }

        public async Task CreateAsync(Transaction transaction)
        {
            await context.Transactions.AddAsync(transaction);

            await context.SaveChangesAsync();
        }
    }
}
