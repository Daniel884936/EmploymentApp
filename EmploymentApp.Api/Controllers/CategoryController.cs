using AutoMapper;
using EmploymentApp.Core.DTOs.CategoryDto;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmploymentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetAll();
            var categoriesReadDto = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            return Ok(categoriesReadDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetById(id);
            var categoryReadDto = _mapper.Map<CategoryReadDto>(category);
            return Ok(categoryReadDto);
        }
        
        [HttpPost]
        public async Task <IActionResult> Post(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryService.Add(category);
            var categoryReadDto = _mapper.Map<CategoryReadDto>(category);
            return Ok(categoryReadDto);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
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
