using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> CreateAsync([FromBody] Transaction transaction)
        {
            if(transaction is null)
            {
                return BadRequest();
            }

            await transactionService.CreateAsync(transaction);

            return StatusCode(201, transaction);
        }
    }
}
