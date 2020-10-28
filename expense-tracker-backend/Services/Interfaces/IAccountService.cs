﻿using expense_tracker_backend.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expense_tracker_backend.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<Account>> GetAllAsync();
        Task<Account> GetByIdAsync(Guid id);
        Task CreateAsync(Account account);
    }
}