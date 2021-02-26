using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Core.DTOs.CategoryDto;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Enums;
using EmploymentApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


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

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryReadDto>>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetCategories()
        {
            ApiResponse<IEnumerable<CategoryReadDto>> response;
            var resultCategory = _categoryService.GetAll();
            if (resultCategory.Status == ResultStatus.Error) 
            {
                response = new ApiResponse<IEnumerable<CategoryReadDto>>(Array.Empty<CategoryReadDto>())
                {
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors = resultCategory.Errors
                }; 
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var categories = resultCategory.Value;
            var categoriesReadDto = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            response = new ApiResponse<IEnumerable<CategoryReadDto>>(categoriesReadDto) { 
                Title = nameof(HttpStatusCode.OK)
            };
            return Ok(response);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Poster")]
        [ProducesResponseType(typeof(ApiResponse<CategoryReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CategoryReadDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategory(int id)
        {
            ApiResponse<CategoryReadDto> response;
            var resultCategory = await _categoryService.GetById(id);
            if(resultCategory.Status == ResultStatus.Error)
            {
                response = new ApiResponse<CategoryReadDto>(null) {
                    Title = nameof(HttpStatusCode.InternalServerError), 
                    Errors = resultCategory.Errors
                }; 
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
            var cartegory = resultCategory.Value;
            var categoryReadDto = _mapper.Map<CategoryReadDto>(cartegory);
            response = new ApiResponse<CategoryReadDto>(categoryReadDto) { 
                Title = nameof(HttpStatusCode.OK)
            }; 
            return Ok(response);
        }

        
        [HttpPost]
        [Authorize(Roles = nameof(Roles.Admin))]
        [ProducesResponseType(typeof(ApiResponse<CategoryReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CategoryReadDto>), StatusCodes.Status500InternalServerError)]
        public async Task <IActionResult> Post(CategoryDto categoryDto)
        {
            ApiResponse<CategoryReadDto> response;
            var category = _mapper.Map<Category>(categoryDto);
            var resultCategory =  await _categoryService.Add(category);
            if (resultCategory.Status == ResultStatus.Error)
            {
                response = new ApiResponse<CategoryReadDto>(null) {
                    Title = nameof(HttpStatusCode.InternalServerError), 
                    Errors = resultCategory.Errors
                };
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
            var categoryReadDto = _mapper.Map<CategoryReadDto>(category);
            response = new ApiResponse<CategoryReadDto>(categoryReadDto) { 
                Title = nameof(HttpStatusCode.OK)
            };
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = nameof(Roles.Admin))]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, CategoryDto categoryDto)
        {
            ApiResponse<bool> response;
            var category = _mapper.Map<Category>(categoryDto);
            category.Id = id;
            var resultCategory =  await _categoryService.Update(category);
            var result = resultCategory.Value;
            if (resultCategory.Status == ResultStatus.Error)
            {
                response = new ApiResponse<bool>(result) { 
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors = resultCategory.Errors
                };
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
            if(resultCategory.Status == ResultStatus.NotFound)
            {
                response = new ApiResponse<bool>(result) { 
                    Title = nameof(HttpStatusCode.NotFound)
                }; 
                return NotFound(response);
            }
            response = new ApiResponse<bool>(result) { Title = nameof(HttpStatusCode.OK) }; 
            return Ok(response);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Roles.Admin))]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Detele(int id)
        {
            ApiResponse<bool> response;
            var resultCategory =  await _categoryService.Remove(id);
            var result = resultCategory.Value;
            if (resultCategory.Status == ResultStatus.Error)
            {
                response = new ApiResponse<bool>(result) {
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors  = resultCategory.Errors
                }; 
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
            if (resultCategory.Status == ResultStatus.NotFound)
            {
                response = new ApiResponse<bool>(result) {
                    Title = nameof(HttpStatusCode.NotFound)
                }; 
                return NotFound(response);
            }
            response = new ApiResponse<bool>(result) { Title = nameof(HttpStatusCode.OK) }; 
            return Ok(response);
        }
    }
}
