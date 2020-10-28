using expense_tracker_backend.Database;
using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return await context.Transactions.ToListAsync();
        }

        public async Task CreateAsync(Transaction transaction)
        {
            await context.Transactions.AddAsync(transaction);

            await context.SaveChangesAsync();
        }
    }
}
