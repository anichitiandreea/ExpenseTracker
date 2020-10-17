using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
    }
}
