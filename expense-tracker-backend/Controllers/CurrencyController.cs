using expense_tracker_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace expense_tracker_backend.Controllers
{
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        [Route("currencies")]
        public async Task<ActionResult> GetAllAsync()
        {
            var currencies = await currencyService.GetAllAsync();

            if(currencies is null)
            {
                return NotFound();
            }

            return Ok(currencies);
        }

        [Route("currencies/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var currency = await currencyService.GetByIdAsync(id);

            if (currency is null)
            {
                return NotFound();
            }

            return Ok(currency);
        }
    }
}
