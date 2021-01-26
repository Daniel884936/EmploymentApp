using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmploymentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetById(id);
            return Ok(category);
        }
        
        [HttpPost]
        public async Task <IActionResult> Post(Category category)
        {
            await _categoryService.Add(category);
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Category category)
        {
            await _categoryService.Update(category);
            return Ok(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Detele(int id)
        {
            await _categoryService.Remove(id);
            return Ok(true);
        }
    }
}
