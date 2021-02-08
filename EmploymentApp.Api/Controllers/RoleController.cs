using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Core.DTOs.RoleDtos;
using EmploymentApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmploymentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleService;
        private readonly IMapper _mapper;
        private string responseMessage;
        public RoleController(IRoleServices roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Roles()
        {
            ApiResponse<IEnumerable<RoleReadDto>> response;
            var resultRoles = _roleService.GetAll();
            if (resultRoles.Status == ResultStatus.Error)
            {
                responseMessage = resultRoles.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<IEnumerable<RoleReadDto>>(Array.Empty<RoleReadDto>(),
                    responseMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var roles = resultRoles.Value;
            var roleReadDto = _mapper.Map<IEnumerable<RoleReadDto>>(roles);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<IEnumerable<RoleReadDto>>(roleReadDto,responseMessage);
            return Ok(response);
        }
    }
}
