using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Core.DTOs.StatusDtos;
using EmploymentApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace EmploymentApp.Api.Controllers
{
    [Authorize(Roles = "Admin,Poster")]
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
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors = resutlStatus.Errors,
                    Satatus = (int)HttpStatusCode.InternalServerError
                }; 
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var status = resutlStatus.Value;
            var statusReadDto = _mapper.Map<IEnumerable<StatusReadDto>>(status);
            response = new  ApiResponse<IEnumerable<StatusReadDto>>(statusReadDto)
            {
                Title = nameof(HttpStatusCode.OK),
                Satatus = (int)HttpStatusCode.OK
            }; 
            return Ok(response);
        }
    }
}
