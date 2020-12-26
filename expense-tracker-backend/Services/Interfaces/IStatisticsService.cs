using expense_tracker_backend.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expense_tracker_backend.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<List<Category>> GetAllCategoriesWithTransactionsAsync();
    }
}
