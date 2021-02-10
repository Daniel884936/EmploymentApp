using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Core.DTOs.JobDtos;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Core.QueryFilters;
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
    public class JobController : ControllerBase
    {
        private readonly IJobService _JobService;
        private readonly IMapper _mapper;
        private string responseMessage;
        public JobController(IJobService JobService, IMapper mapper)
        {
            _JobService = JobService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetJobs([FromQuery]JobQueryFilter jobQueryFilter)
        {
            ApiResponse<IEnumerable<JobReadDto>> response;
            var resultJob = _JobService.GetAll(jobQueryFilter);
            if (resultJob.Status == ResultStatus.Error)
            {
                responseMessage = resultJob.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<IEnumerable<JobReadDto>>(Array.Empty<JobReadDto>(),
                    responseMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var jobs = resultJob.Value;
            var jobsReadDto = _mapper.Map<IEnumerable<JobReadDto>>(jobs);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<IEnumerable<JobReadDto>>(jobsReadDto, responseMessage);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            ApiResponse<JobReadDto> response;
            var resultJob = await _JobService.GetById(id);
            if (resultJob.Status == ResultStatus.Error)
            {
                responseMessage = resultJob.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<JobReadDto>(null,responseMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var job = resultJob.Value;
            var jobReadDto = _mapper.Map<JobReadDto>(job);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<JobReadDto>(jobReadDto,responseMessage);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(JobDto jobDto)
        {
            ApiResponse<JobReadDto> response;
            var job = _mapper.Map<Job>(jobDto);
            var resultJob = await _JobService.Add(job);
            if (resultJob.Status == ResultStatus.Error)
            {
                responseMessage = resultJob.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<JobReadDto>(null,responseMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var jobReadDto = _mapper.Map<JobReadDto>(job);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<JobReadDto>(jobReadDto,responseMessage);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, JobDto jobDto)
        {
            ApiResponse<bool> response;
            var job = _mapper.Map<Job>(jobDto);
            job.Id = id;
            var resultJob = await _JobService.Update(job);
            var result = resultJob.Value;
            if (resultJob.Status == ResultStatus.Error)
            {
                responseMessage = resultJob.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<bool>(result,responseMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultJob.Status == ResultStatus.NotFound)
            {
                responseMessage = StringResponseMessages.DOES_NOT_EXIST;
                response = new ApiResponse<bool>(result,responseMessage);
                return NotFound(response);
            }
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<bool>(result,responseMessage);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Detele(int id)
        {
            ApiResponse<bool> response;
            var resultJob = await _JobService.Remove(id);
            var result = resultJob.Value;
            if (resultJob.Status == ResultStatus.Error)
            {
                responseMessage = resultJob.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<bool>(result,responseMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultJob.Status == ResultStatus.NotFound)
            {
                responseMessage = StringResponseMessages.DOES_NOT_EXIST;
                response = new ApiResponse<bool>(result, responseMessage);
                return NotFound(response);
            }
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<bool>(result,responseMessage);
            return Ok(response);
        }
    }
}
