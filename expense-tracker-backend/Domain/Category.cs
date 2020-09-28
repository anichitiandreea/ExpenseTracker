using expense_tracker_backend.Domain.Enums;
using System.Collections.Generic;

namespace expense_tracker_backend.Domain
{
    public class Category : Entity
    {
        public Currency Currency { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
