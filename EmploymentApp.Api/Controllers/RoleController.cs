using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Core.DTOs.RoleDtos;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Roles()
        {
            var resultRoles = _roleService.GetAll();
            if (resultRoles.Status == ResultStatus.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<IEnumerable<Role>>(Array.Empty<Role>(),
                    resultRoles.Errors.ToList()[(int)ErrorNum.First]));
            }
            var roles = resultRoles.Value;
            var roleReadDto = _mapper.Map<IEnumerable<RoleReadDto>>(roles);
            var response = new ApiResponse<IEnumerable<RoleReadDto>>(roleReadDto,
                StringResponseMessages.SUCESS);
            return Ok(response);
        }
    }
}
