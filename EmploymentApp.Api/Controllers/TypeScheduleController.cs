using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Core.DTOs.TypeScheduleDto;
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
            var resutlTypeSchedule = _typeScheduleService.GetAll();
            if (resutlTypeSchedule.Status == ResultStatus.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<IEnumerable<TypeSchedule>>(Array.Empty<TypeSchedule>(),
                    resutlTypeSchedule.Errors.ToList()[(int)ErrorNum.First]));
            }
            var typeSchedule = resutlTypeSchedule.Value;
            var typeScheduleReadDto = _mapper.Map<IEnumerable<TypeScheduleReadDto>>(typeSchedule);
            var response = new ApiResponse<IEnumerable<TypeScheduleReadDto>>(typeScheduleReadDto,
                StringResponseMessages.SUCESS);
            return Ok(response);
        }
    }
}
