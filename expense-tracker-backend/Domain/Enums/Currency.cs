using System;

namespace expense_tracker_backend.Domain.Enums
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
