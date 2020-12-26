using expense_tracker_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace expense_tracker_backend.Controllers
{
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        [Route("statistics/category-expense")]
        public async Task<ActionResult> GetCategoryExpenseAsync([FromQuery] DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var categoriesWithTransactions = await statisticsService.GetAllCategoriesWithTransactionsAsync();

                if (categoriesWithTransactions is null)
                {
                    return NotFound();
                }

                var categoriesAmounts = categoriesWithTransactions.Select(category =>
                {
                    double totalAmount = 0;
                    category.Transactions.ForEach(transaction =>
                    {
                        if (transaction.TransactionDate >= fromDate && transaction.TransactionDate <= toDate)
                        {
                            totalAmount += transaction.Amount;
                        }
                    });

                    return new { CategoryName = category.Name, CategoryAmount = totalAmount };
                })
                .ToList();

                return Ok(categoriesAmounts);
            }
            catch(Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
