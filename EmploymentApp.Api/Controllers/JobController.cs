using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.DTOs.JobDtos;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Core.QueryFilters;
using EmploymentApp.Infrastructure.Interfaces;
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
        private readonly IUriService _uriService;
        public JobController(IJobService JobService, IMapper mapper, IUriService uriService)
        {
            _JobService = JobService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<JobReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetJobs([FromQuery]JobQueryFilter filter)
        {
            ApiResponse<IEnumerable<JobReadDto>> response;
            var resultJob = _JobService.GetAll(filter);
            if (resultJob.Status == ResultStatus.Error)
            {
                responseMessage = resultJob.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiPagedResponse<IEnumerable<JobReadDto>>(Array.Empty<JobReadDto>()) { 
                    Message = responseMessage 
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var jobs = resultJob.Value;
            var meta = _mapper.Map<Metadata>(jobs);
            meta.NextPageUrl = _uriService.GetPaginationNextUrl(filter, Request, meta.HasNextPage);
            meta.PreviousPageUrl = _uriService.GetPaginationPreviousUrl(filter, Request, meta.HasPreviousPage);
            var jobsReadDto = _mapper.Map<IEnumerable<JobReadDto>>(jobs);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiPagedResponse<IEnumerable<JobReadDto>>(jobsReadDto)
            {
                Message = responseMessage,
                Meta = meta
            }; 
            return Ok(response);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<JobReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJob(int id)
        {
            ApiResponse<JobReadDto> response;
            var resultJob = await _JobService.GetById(id);
            if (resultJob.Status == ResultStatus.Error)
            {
                responseMessage = resultJob.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<JobReadDto>(null) { Message = responseMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var job = resultJob.Value;
            var jobReadDto = _mapper.Map<JobReadDto>(job);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<JobReadDto>(jobReadDto) { Message = responseMessage }; 
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<JobReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(JobDto jobDto)
        {
            ApiResponse<JobReadDto> response;
            var job = _mapper.Map<Job>(jobDto);
            var resultJob = await _JobService.Add(job);
            if (resultJob.Status == ResultStatus.Error)
            {
                responseMessage = resultJob.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<JobReadDto>(null) { Message = responseMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var jobReadDto = _mapper.Map<JobReadDto>(job);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<JobReadDto>(jobReadDto) { Message = responseMessage }; 
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                response = new ApiResponse<bool>(result) { Message = responseMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultJob.Status == ResultStatus.NotFound)
            {
                responseMessage = StringResponseMessages.DOES_NOT_EXIST;
                response = new ApiResponse<bool>(result) { Message = responseMessage };
                return NotFound(response);
            }
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<bool>(result) { Message = responseMessage };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Detele(int id)
        {
            ApiResponse<bool> response;
            var resultJob = await _JobService.Remove(id);
            var result = resultJob.Value;
            if (resultJob.Status == ResultStatus.Error)
            {
                responseMessage = resultJob.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<bool>(result) { Message = responseMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultJob.Status == ResultStatus.NotFound)
            {
                responseMessage = StringResponseMessages.DOES_NOT_EXIST;
                response = new ApiResponse<bool>(result) { Message = responseMessage };
                return NotFound(response);
            }
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<bool>(result) { Message = responseMessage };
            return Ok(response);
        }
    }
}
