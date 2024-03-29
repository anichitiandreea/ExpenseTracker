﻿using ExpenseTracker.Domain;
using ExpenseTracker.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Interfaces
{
    public interface ITransactionService
    {
        event Func<object, TransactionCreatedEventArgs, Task> TransactionCreated;
        Task<List<Transaction>> GetAllAsync(int pageNumber, int pageSize);
        Task<Transaction> GetByIdAsync(Guid id);
        Task<List<Transaction>> GetByCategoryIdAsync(Guid categoryId, DateTime? fromDate, DateTime? toDate);
        Task<List<Transaction>> GetByCategoryIdAsync(Guid categoryId);
        Task CreateAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
    }
}
