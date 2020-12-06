using expense_tracker_backend.Domain;
using expense_tracker_backend.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static expense_tracker_backend.Services.TransactionService;

namespace expense_tracker_backend.Services.Interfaces
{
    public interface ITransactionService
    {
        event Func<object, TransactionCreatedEventArgs, Task> TransactionCreated;
        Task<List<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(Guid id);
        Task<List<Transaction>> GetByCategoryIdAsync(Guid categoryId, DateTime? fromDate, DateTime? toDate);
        Task CreateAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
    }
}
