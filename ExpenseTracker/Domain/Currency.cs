using System;

namespace ExpenseTracker.Domain.Domains
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
