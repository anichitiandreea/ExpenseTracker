using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
using expense_tracker_backend.Transfer;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var transactions = await transactionService.GetAllAsync();

            if(transactions is null)
            {
                return NotFound();
            }

            return Ok(transactions);
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
                    CategoryId = transactionRequest.CategoryId
                };

                await transactionService.CreateAsync(transaction);

                return StatusCode(201, transaction);
            }
            catch(Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
