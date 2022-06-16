using ExpenseTracker.Domain;
using System;

namespace ExpenseTracker.Events
{
    public class TransactionCreatedEventArgs : EventArgs
    {
        public Transaction Transaction { get; set; }
    }
}
