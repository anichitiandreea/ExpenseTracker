using System;

namespace expense_tracker_backend.Transfer
{
    public class AccountRequest
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Amount { get; set; }
        public Guid CurrencyId { get; set; }
    }
}
