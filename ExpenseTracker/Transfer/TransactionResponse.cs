using ExpenseTracker.Domain.Enums;
using System;

namespace ExpenseTracker.Transfer
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string CurrencyName { get; set; }
        public string Note { get; set; }
        public CategoryResponse Category { get; set; }
        public AccountResponse Account { get; set; }
    }
}
