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
        private string responseMessage;
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
                responseMessage = resutlTypeSchedule.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<IEnumerable<TypeScheduleReadDto>>(Array.Empty<TypeScheduleReadDto>(),
                    responseMessage);
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
            var typeSchedule = resutlTypeSchedule.Value;
            var typeScheduleReadDto = _mapper.Map<IEnumerable<TypeScheduleReadDto>>(typeSchedule);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<IEnumerable<TypeScheduleReadDto>>(typeScheduleReadDto,
               responseMessage);
            return Ok(response);
        }
    }
}
