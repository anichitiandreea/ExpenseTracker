using System;
using System.Collections.Generic;

namespace ExpenseTracker.Domain
{
    public class Category : Entity
    {
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Name { get; set; }
        public string CurrencyName { get; set; }
        public Guid CurrencyId { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
