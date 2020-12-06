using expense_tracker_backend.Domain;
using System;

namespace expense_tracker_backend.Events
{
    public class TransactionCreatedEventArgs : EventArgs
    {
        public Transaction Transaction { get; set; }
    }
}
