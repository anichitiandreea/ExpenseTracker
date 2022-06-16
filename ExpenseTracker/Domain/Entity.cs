using System;

namespace ExpenseTracker.Domain
{
    public class Entity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
