using ExpenseTracker.Domain;
using ExpenseTracker.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<Account>> GetAllAsync();
        Task<Account> GetByIdAsync(Guid id);
        Task CreateAsync(Account account);
        Task UpdateAsync(Account account);
        Task UpdateAccountAmountAsync(object sender, TransactionCreatedEventArgs e);
        Task DeleteAsync(Account account);
    }
}
