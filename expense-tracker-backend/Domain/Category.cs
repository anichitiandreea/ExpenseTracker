using expense_tracker_backend.Domain.Domains;
using expense_tracker_backend.Domain.Enums;
using System;
using System.Collections.Generic;

namespace expense_tracker_backend.Domain
{
    public class Category : Entity
    {
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Name { get; set; }
        public Guid CurrencyId { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
