using System;

namespace expense_tracker_backend.Transfer
{
    public class AccountRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Amount { get; set; }
        public string CurrencyName { get; set; }
        public Guid CurrencyId { get; set; }
    }
}
