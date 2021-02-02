using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Core.DTOs.StatusDtos;
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
        public IActionResult Status()
        {
            var resutlStatus = _statusService.GetAll();
            if(resutlStatus.Status == ResultStatus.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<IEnumerable<Status>>(Array.Empty<Status>(),
                    resutlStatus.Errors.ToList()[(int)ErrorNum.First]));
            }
            var status = resutlStatus.Value;
            var statusReadDto = _mapper.Map<IEnumerable<StatusReadDto>>(status);
            var response = new  ApiResponse<IEnumerable<StatusReadDto>>(statusReadDto,
                StringResponseMessages.SUCESS);
            return Ok(response);
        }
    }
}
