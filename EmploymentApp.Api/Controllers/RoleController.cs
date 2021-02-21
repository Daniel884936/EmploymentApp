using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Core.DTOs.RoleDtos;
using EmploymentApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace EmploymentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleService;
        private readonly IMapper _mapper;
        public RoleController(IRoleServices roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleReadDto>>), StatusCodes.Status500InternalServerError)]
        public IActionResult Roles()
        {
            ApiResponse<IEnumerable<RoleReadDto>> response;
            var resultRoles = _roleService.GetAll();
            if (resultRoles.Status == ResultStatus.Error)
            {
                response = new ApiResponse<IEnumerable<RoleReadDto>>(Array.Empty<RoleReadDto>()) 
                {
                    Message = nameof(HttpStatusCode.InternalServerError), 
                    Errors  = resultRoles.Errors
                }; 
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var roles = resultRoles.Value;
            var roleReadDto = _mapper.Map<IEnumerable<RoleReadDto>>(roles);
            response = new ApiResponse<IEnumerable<RoleReadDto>>(roleReadDto)
            {
                Message = nameof(HttpStatusCode.OK)
            };
            return Ok(response);
        }
    }
}
