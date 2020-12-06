using expense_tracker_backend.Database;
using expense_tracker_backend.Domain;
using expense_tracker_backend.Events;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker_backend.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DatabaseContext context;
        public event Func<object, TransactionCreatedEventArgs, Task> TransactionCreated;

        public TransactionService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await context.Transactions
                .Include(transaction => transaction.Account)
                .Include(transaction => transaction.Category)
                .OrderByDescending(transaction => transaction.TransactionDate)
                .ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            return await context.Transactions
                .Include(transaction => transaction.Account)
                .Include(transaction => transaction.Category)
                .FirstOrDefaultAsync(transaction =>
                    transaction.Id == id
                    && !transaction.IsDeleted);
        }

        public async Task<List<Transaction>> GetByCategoryIdAsync(Guid categoryId, DateTime? fromDate, DateTime? toDate)
        {
            return await context.Transactions
                .Where(transaction =>
                    transaction.CategoryId == categoryId
                    && transaction.TransactionDate >= fromDate
                    && transaction.TransactionDate < toDate
                    && !transaction.IsDeleted)
                .ToListAsync();
        }

        public async Task CreateAsync(Transaction transaction)
        {
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            TransactionCreatedEventArgs args = new TransactionCreatedEventArgs
            {
                Transaction = transaction
            };

            TransactionCreated?.Invoke(this, args);
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();
        }
    }
}
