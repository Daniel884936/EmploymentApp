using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Core.DTOs.TypeScheduleDto;
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
        public IActionResult TypeSchedules()
        {
            ApiResponse<IEnumerable<TypeScheduleReadDto>> response;
            var resutlTypeSchedule = _typeScheduleService.GetAll();
            if (resutlTypeSchedule.Status == ResultStatus.Error)
            {
                response = new ApiResponse<IEnumerable<TypeScheduleReadDto>>(Array.Empty<TypeScheduleReadDto>(),
                    resutlTypeSchedule.Errors.ToList()[(int)ErrorNum.First]);
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
            var typeSchedule = resutlTypeSchedule.Value;
            var typeScheduleReadDto = _mapper.Map<IEnumerable<TypeScheduleReadDto>>(typeSchedule);
            response = new ApiResponse<IEnumerable<TypeScheduleReadDto>>(typeScheduleReadDto,
                StringResponseMessages.SUCESS);
            return Ok(response);
        }
    }
}
