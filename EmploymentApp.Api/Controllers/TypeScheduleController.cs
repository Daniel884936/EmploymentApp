using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Core.DTOs.TypeScheduleDto;
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
    public class TypeScheduleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITypeScheduleService _typeScheduleService;
        public TypeScheduleController(IMapper mapper, ITypeScheduleService typeScheduleService)
        {
            _mapper = mapper;
            _typeScheduleService = typeScheduleService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TypeScheduleReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TypeScheduleReadDto>>), StatusCodes.Status500InternalServerError)]
        public IActionResult TypeSchedules()
        {
            ApiResponse<IEnumerable<TypeScheduleReadDto>> response;
            var resutlTypeSchedule = _typeScheduleService.GetAll();
            if (resutlTypeSchedule.Status == ResultStatus.Error)
            {
                response = new ApiResponse<IEnumerable<TypeScheduleReadDto>>(Array.Empty<TypeScheduleReadDto>()) 
                { 
                    Title = nameof(HttpStatusCode.InternalServerError), 
                    Errors = resutlTypeSchedule.Errors,
                    Satatus = (int)HttpStatusCode.InternalServerError
                }; 
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
            var typeSchedule = resutlTypeSchedule.Value;
            var typeScheduleReadDto = _mapper.Map<IEnumerable<TypeScheduleReadDto>>(typeSchedule);
            response = new ApiResponse<IEnumerable<TypeScheduleReadDto>>(typeScheduleReadDto) 
            { 
                Title = nameof(HttpStatusCode.OK),
                Satatus = (int)HttpStatusCode.OK
            }; 
            return Ok(response);
        }
    }
}
