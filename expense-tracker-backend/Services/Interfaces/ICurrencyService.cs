using expense_tracker_backend.Domain.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expense_tracker_backend.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetAllAsync();
    }
}
