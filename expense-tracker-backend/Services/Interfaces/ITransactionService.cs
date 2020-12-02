﻿using expense_tracker_backend.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expense_tracker_backend.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(Guid id);
        Task<List<Transaction>> GetByCategoryIdAsync(Guid categoryId, DateTime? fromDate, DateTime? toDate);
        Task CreateAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
    }
}
