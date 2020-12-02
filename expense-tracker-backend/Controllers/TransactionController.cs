using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
using expense_tracker_backend.Transfer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace expense_tracker_backend.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        [Route("transactions")]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var transactions = await transactionService.GetAllAsync();

                if (transactions is null)
                {
                    return NotFound();
                }

                var transactionResponse = transactions.Select(transaction => new TransactionResponse
                {
                    Id = transaction.Id,
                    TransactionDate = transaction.TransactionDate,
                    TransactionType = transaction.TransactionType,
                    Amount = transaction.Amount,
                    CurrencyName = transaction.CurrencyName,
                    Note = transaction.Note,
                    Category = new CategoryResponse() {
                        Name = transaction.Category.Name,
                        Icon = transaction.Category.Icon,
                        IconColor = transaction.Category.IconColor
                    },
                    Account = new AccountResponse() {
                        Name = transaction.Account.Name,
                        Icon = transaction.Account.Icon,
                        IconColor = transaction.Account.IconColor
                    }
                })
                .ToList();

                var transactionGroup = transactionResponse.GroupBy(
                    t => t.TransactionDate,
                    t => t,
                    (date, transactions) => new { TransactionDate = date, Transactions = transactions.ToList() });

                return Ok(transactionGroup);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [HttpGet]
        [Route("transactions/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var transaction = await transactionService.GetByIdAsync(id);

                if (transaction is null)
                {
                    return NotFound();
                }

                var transactionResponse = new TransactionResponse
                {
                    Id = transaction.Id,
                    TransactionDate = transaction.TransactionDate,
                    TransactionType = transaction.TransactionType,
                    Amount = transaction.Amount,
                    CurrencyName = transaction.CurrencyName,
                    Note = transaction.Note,
                    Category = new CategoryResponse()
                    {
                        Id = transaction.Category.Id,
                        Name = transaction.Category.Name,
                        Icon = transaction.Category.Icon,
                        IconColor = transaction.Category.IconColor
                    },
                    Account = new AccountResponse()
                    {
                        Id = transaction.Account.Id,
                        Name = transaction.Account.Name,
                        Icon = transaction.Account.Icon,
                        IconColor = transaction.Account.IconColor
                    }
                };

                return Ok(transactionResponse);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [HttpGet]
        [Route("categories/{categoryId}/transactions/total-expense")]
        public async Task<ActionResult> GetMonthExpenseAsync([FromRoute] Guid categoryId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                double categoryExpense = 0;
                var transactions = await transactionService.GetByCategoryIdAsync(categoryId, fromDate, toDate);

                if (transactions is null)
                {
                    return NotFound();
                }

                transactions
                    .ForEach(transaction =>
                    {
                        categoryExpense += transaction.Amount;
                    });

                return Ok(categoryExpense);

            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [HttpPost]
        [Route("transactions")]
        public async Task<ActionResult> CreateAsync([FromBody] TransactionRequest transactionRequest)
        {
            try
            {
                if (transactionRequest is null)
                {
                    return BadRequest();
                }

                var transaction = new Transaction
                {
                    TransactionDate = transactionRequest.TransactionDate,
                    TransactionType = transactionRequest.TransactionType,
                    Amount = Convert.ToDouble(transactionRequest.Amount),
                    Note = transactionRequest.Note,
                    AccountId = transactionRequest.AccountId,
                    CategoryId =  transactionRequest.CategoryId,
                    CurrencyName = transactionRequest.CurrencyName
                };

                await transactionService.CreateAsync(transaction);

                return StatusCode(201, transaction);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [HttpPut]
        [Route("transactions")]
        public async Task<IActionResult> UpdateAsync(Transaction transaction)
        {
            try
            {
                await transactionService.UpdateAsync(transaction);

                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
