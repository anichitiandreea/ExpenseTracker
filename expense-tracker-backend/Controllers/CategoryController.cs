using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace expense_tracker_backend.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult> GetAllAsync()
        {
            var categories = await categoryService.GetAllAsync();

            if(categories is null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpGet]
        [Route("categories/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var category = await categoryService.GetByIdAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [Route("categories")]
        public async Task<ActionResult> CreateAsync([FromBody] Category category)
        {
            if(category is null)
            {
                return BadRequest();
            }

            await categoryService.CreateAsync(category);

            return StatusCode(201, category);
        }

        [HttpPut]
        [Route("categories")]
        public async Task<ActionResult> UpdateAsync([FromBody] Category category)
        {
            if (category is null)
            {
                return BadRequest();
            }

            var oldCategory = await categoryService.GetByIdAsync(category.Id);

            if (oldCategory is null)
            {
                return NotFound();
            }

            oldCategory.Name = category.Name;
            oldCategory.Icon = category.Icon;
            oldCategory.IconColor = category.IconColor;
            oldCategory.CurrencyId = category.CurrencyId;

            await categoryService.UpdateAsync(oldCategory);

            return Ok(oldCategory);
        }
    }
}
