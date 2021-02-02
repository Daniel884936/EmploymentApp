using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Core.DTOs.CategoryDto;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ardalis.Result;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EmploymentApp.Api.Source;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;


namespace EmploymentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        //TODO: resolver el problema del mesaje de retorno del status 500 y terminar los metodos restantes
        [HttpGet]
        public IActionResult GetCategories()
        {
            var resultCategory = _categoryService.GetAll();
            //Check if there are any arror 
            if (resultCategory.Status == ResultStatus.Error) {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new ApiResponse<IEnumerable<CategoryReadDto>>(Array.Empty<CategoryReadDto>(), 
                    resultCategory.Errors.ToList()[(int)ErrorNum.First]));
            }

            var categories = resultCategory.Value;
            var categoriesReadDto = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            var response = new ApiResponse<IEnumerable<CategoryReadDto>>(categoriesReadDto,
                StringResponseMessages.SUCESS);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var resultCategory = await _categoryService.GetById(id);

            if(resultCategory.Status == ResultStatus.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<CategoryReadDto>(null,
                    resultCategory.Errors.ToList()[(int)ErrorNum.First]));
            }

            var cartegory = resultCategory.Value;
            var categoryReadDto = _mapper.Map<CategoryReadDto>(cartegory);
            var response = new ApiResponse<CategoryReadDto>(categoryReadDto,
                StringResponseMessages.SUCESS);
            return Ok(response);
        }
        
        [HttpPost]
        public async Task <IActionResult> Post(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var resultCategory =  await _categoryService.Add(category);
            if (resultCategory.Status == ResultStatus.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<CategoryReadDto>(null,
                    resultCategory.Errors.ToList()[(int)ErrorNum.First]));
            }

            var categoryReadDto = _mapper.Map<CategoryReadDto>(category);
            var response = new ApiResponse<CategoryReadDto>(categoryReadDto,
                 StringResponseMessages.SUCESS);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.Id = id;
            var resultCategory =  await _categoryService.Update(category);

            if (resultCategory.Status == ResultStatus.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<bool>(false,
                    resultCategory.Errors.ToList()[(int)ErrorNum.First]));
            }
            if(resultCategory.Status == ResultStatus.NotFound)
            {
                return NotFound(new ApiResponse<CategoryReadDto>(null, 
                    StringResponseMessages.DOES_NOT_EXIST));
            }
            var result = resultCategory.Value;
            var response = new ApiResponse<bool>(result,
                 StringResponseMessages.SUCESS);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Detele(int id)
        {
            var resultCategory =  await _categoryService.Remove(id);

            if (resultCategory.Status == ResultStatus.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<bool>(false,
                    resultCategory.Errors.ToList()[(int)ErrorNum.First]));
            }

            if (resultCategory.Status == ResultStatus.NotFound)
            {
                return NotFound(new ApiResponse<CategoryReadDto>(null,
                    StringResponseMessages.DOES_NOT_EXIST));
            }
            var result = resultCategory.Value;
            var response = new ApiResponse<bool>(result,
                 StringResponseMessages.SUCESS);
            return Ok(response);
        }
    }
}
