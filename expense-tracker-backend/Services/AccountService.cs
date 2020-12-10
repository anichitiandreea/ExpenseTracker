using expense_tracker_backend.Database;
using expense_tracker_backend.Domain;
using expense_tracker_backend.Domain.Enums;
using expense_tracker_backend.Events;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expense_tracker_backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext context;

        public AccountService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await context.Accounts.ToListAsync();
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await context.Accounts
                .FirstOrDefaultAsync(account =>
                    account.Id == id
                    && account.IsDeleted == false);
        }

        public async Task CreateAsync(Account account)
        {
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            context.Accounts.Update(account);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAccountAmountAsync(object sender, TransactionCreatedEventArgs e)
        {
            var account = await context.Accounts.FirstOrDefaultAsync(account =>
                account.Id == e.Transaction.AccountId
                && !account.IsDeleted);

            if (e.Transaction.TransactionType == TransactionType.Expense)
            {
                account.Amount -= e.Transaction.Amount;
            }
            else
            {
                account.Amount += e.Transaction.Amount;
            }

            context.Accounts.Update(account);
            await context.SaveChangesAsync();
        }
    }
}
