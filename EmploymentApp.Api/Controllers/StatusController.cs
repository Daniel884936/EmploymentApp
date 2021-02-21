using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Core.DTOs.StatusDtos;
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
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;
        public StatusController(IStatusService statusService, IMapper mapper)
        {
            _statusService = statusService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<StatusReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<StatusReadDto>>), StatusCodes.Status500InternalServerError)]
        public IActionResult Status()
        {
           ApiResponse<IEnumerable<StatusReadDto>> response;
           var resutlStatus = _statusService.GetAll();
            if(resutlStatus.Status == ResultStatus.Error)
            {
                response = new ApiResponse<IEnumerable<StatusReadDto>>(Array.Empty<StatusReadDto>())
                {
                    Message = nameof(HttpStatusCode.InternalServerError),
                    Errors = resutlStatus.Errors
                }; 
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var status = resutlStatus.Value;
            var statusReadDto = _mapper.Map<IEnumerable<StatusReadDto>>(status);
            response = new  ApiResponse<IEnumerable<StatusReadDto>>(statusReadDto)
            {
                Message = nameof(HttpStatusCode.OK)
            }; 
            return Ok(response);
        }
    }
}
