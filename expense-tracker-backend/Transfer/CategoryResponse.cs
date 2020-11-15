using System;

namespace expense_tracker_backend.Transfer
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
    }
}
