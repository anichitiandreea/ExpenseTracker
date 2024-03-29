﻿using ExpenseTracker.Database;
using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Events;
using ExpenseTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
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

        public async Task UpdateRangeAsync(HashSet<Account> accounts)
        {
            context.Accounts.UpdateRange(accounts);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Account account)
        {
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
        }
    }
}
