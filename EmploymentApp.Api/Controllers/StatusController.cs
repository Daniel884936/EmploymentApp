using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Core.DTOs.StatusDtos;
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
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;
        private string responseMessage;
        public StatusController(IStatusService statusService, IMapper mapper)
        {
            _statusService = statusService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Status()
        {
           ApiResponse<IEnumerable<StatusReadDto>> response;
           var resutlStatus = _statusService.GetAll();
            if(resutlStatus.Status == ResultStatus.Error)
            {
                responseMessage = resutlStatus.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<IEnumerable<StatusReadDto>>(Array.Empty<StatusReadDto>(),
                    responseMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var status = resutlStatus.Value;
            var statusReadDto = _mapper.Map<IEnumerable<StatusReadDto>>(status);
            responseMessage = StringResponseMessages.SUCESS;
            response = new  ApiResponse<IEnumerable<StatusReadDto>>(statusReadDto,responseMessage);
            return Ok(response);
        }
    }
}
