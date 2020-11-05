using expense_tracker_backend.Domain.Enums;
using System;

namespace expense_tracker_backend.Transfer
{
    public class TransactionResponse
    {
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public string CategoryName { get; set; }
        public string AccountName { get; set; }
    }
}
