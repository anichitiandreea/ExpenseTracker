using System.Collections.Generic;

namespace expense_tracker_backend.Domain
{
    public class Account : Entity
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
