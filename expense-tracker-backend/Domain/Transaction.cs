using expense_tracker_backend.Domain.Enums;
using System;

namespace expense_tracker_backend.Domain
{
    public class Transaction : Entity
    {
        public TransactionType TransactionType { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public Guid CategoryId { get; set; }
    }
}
