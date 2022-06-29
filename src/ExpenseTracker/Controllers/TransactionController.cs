using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Services.Interfaces;
using ExpenseTracker.Transfer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IAccountService accountService;

        public TransactionController(ITransactionService transactionService, IAccountService accountService)
        {
            this.transactionService = transactionService;
            this.accountService = accountService;
        }

        [HttpGet]
        [Route("transactions")]
        public async Task<ActionResult> GetAllAsync([FromQuery] int pageNumber, int pageSize)
        {
            try
            {
                var transactions = await transactionService.GetAllAsync(pageNumber, pageSize);

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

                var account = await accountService.GetByIdAsync(transaction.AccountId);

                if (transaction.TransactionType == TransactionType.Expense)
                {
                    account.Amount -= transaction.Amount;
                }
                else
                {
                    account.Amount += transaction.Amount;
                }

                await transactionService.CreateAsync(transaction);
                await accountService.UpdateAsync(account);

                return StatusCode(201);
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
                double transactionAmountDifference = 0;
                var originalTransaction = await transactionService.GetByIdAsync(transaction.Id);

                if (transaction.Amount > originalTransaction.Amount)
                {
                    transactionAmountDifference = transaction.Amount - originalTransaction.Amount;
                }
                else
                {
                    transactionAmountDifference = originalTransaction.Amount - transaction.Amount;
                }

                var account = await accountService.GetByIdAsync(transaction.AccountId);
                account.Amount -= transactionAmountDifference;

                await transactionService.UpdateAsync(transaction);
                await accountService.UpdateAsync(account);

                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [HttpDelete]
        [Route("transactions")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var transaction = await transactionService.GetByIdAsync(id);

                if (transaction is null)
                {
                    return NotFound();
                }

                transaction.Account.Amount += transaction.Amount;

                await transactionService.DeleteAsync(transaction);
                await accountService.UpdateAsync(transaction.Account);

                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
