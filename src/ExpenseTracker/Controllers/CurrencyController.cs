using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using ExpenseTracker.Domain.Domains;

namespace ExpenseTracker.Controllers
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
            try
            {
                var currencies = await currencyService.GetAllAsync();

                if (currencies is null)
                {
                    return NotFound();
                }

                return Ok(currencies);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [Route("currencies/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var currency = await currencyService.GetByIdAsync(id);

                if (currency is null)
                {
                    return NotFound();
                }

                return Ok(currency);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [HttpPost]
        [Route("currencies")]
        public async Task<ActionResult> CreateAsync()
        {
            try
            {
                var currency = new Currency();
                currency.Id = new Guid();
                currency.Name = "Eur";
                currency.Description = "Euro";

                await currencyService.CreateAsync(currency);

                return StatusCode(201, currency);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

    }
}
