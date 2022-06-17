using ExpenseTracker.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<List<Category>> GetAllCategoriesWithTransactionsAsync();
    }
}
