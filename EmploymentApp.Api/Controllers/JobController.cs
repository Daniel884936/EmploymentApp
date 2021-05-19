using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.services.Image;
using EmploymentApp.Api.Source;
using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.DTOs.JobDtos;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Enums;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Core.QueryFilters;
using EmploymentApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EmploymentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _JobService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IImageService _imageService;

        public JobController(IJobService JobService, IMapper mapper, IUriService uriService, IImageService imageService)
        {
            _JobService = JobService;
            _mapper = mapper;
            _uriService = uriService;
            _imageService = imageService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<JobReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<JobReadDto>>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetJobs([FromQuery] JobQueryFilter filter)
        {
            ApiResponse<IEnumerable<JobReadDto>> response;
            var resultJob = _JobService.GetAll(filter);
            if (resultJob.Status == ResultStatus.Error)
            {
                response = new ApiPagedResponse<IEnumerable<JobReadDto>>(Array.Empty<JobReadDto>()) {
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors = resultJob.Errors,
                    Satatus = (int)HttpStatusCode.InternalServerError
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var jobs = resultJob.Value;
            var meta = _mapper.Map<Metadata>(jobs);
            meta.NextPageUrl = _uriService.GetPaginationNextUrl(filter, Request, meta.HasNextPage);
            meta.PreviousPageUrl = _uriService.GetPaginationPreviousUrl(filter, Request, meta.HasPreviousPage);
            var jobsReadDto = _mapper.Map<IEnumerable<JobReadDto>>(jobs);
            response = new ApiPagedResponse<IEnumerable<JobReadDto>>(jobsReadDto)
            {
                Title = nameof(HttpStatusCode.OK),
                Meta = meta,
                Satatus = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Roles.Admin))]
        [ProducesResponseType(typeof(ApiResponse<JobReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<JobReadDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJob(int id)
        {
            ApiResponse<JobReadDto> response;
            var resultJob = await _JobService.GetById(id);

            if (resultJob.Status == ResultStatus.Error)
            {
                response = new ApiResponse<JobReadDto>(null) {
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors = resultJob.Errors,
                    Satatus = (int)HttpStatusCode.InternalServerError
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var job = resultJob.Value;
            var jobReadDto = _mapper.Map<JobReadDto>(job);
            response = new ApiResponse<JobReadDto>(jobReadDto) {
                Title = nameof(HttpStatusCode.OK),
                Satatus = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Poster")]
        [ProducesResponseType(typeof(ApiResponse<JobReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<JobReadDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] JobDto jobDto)
        {
            ApiResponse<JobReadDto> response;
            var job = _mapper.Map<Job>(jobDto);
            string imgUrl;
            if (jobDto.Img != null)
            {
                imgUrl = await _imageService.Save(jobDto.Img, 
                    AplicationConstants.FileContainers.JobImageContainer);
                job.Img = imgUrl;
            }
            var resultJob = await _JobService.Add(job);
            if (resultJob.Status == ResultStatus.Error)
            {
                response = new ApiResponse<JobReadDto>(null) {
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors = resultJob.Errors,
                    Satatus = (int)HttpStatusCode.InternalServerError
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var jobReadDto = _mapper.Map<JobReadDto>(job);
            response = new ApiResponse<JobReadDto>(jobReadDto) {
                Title = nameof(HttpStatusCode.OK),
                Satatus = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }


        [HttpPut]
        [Authorize(Roles = "Admin,Poster")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromForm] JobDto jobDto)
        {
            ApiResponse<bool> response;
            var job = _mapper.Map<Job>(jobDto);
            job.Id = id;
            var resultJobImage = await _JobService.GetById(id);
            var jobImage = resultJobImage.Value;
            if (jobImage != null)
            {
                job.Img = await _imageService.Update(jobImage.Img, jobDto.Img,
                    AplicationConstants.FileContainers.JobImageContainer);
            }
            var resultJob = await _JobService.Update(job);
            var result = resultJob.Value;
            if (resultJob.Status == ResultStatus.Error || resultJobImage.Status == ResultStatus.Error)
            {
                response = new ApiResponse<bool>(result) {
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors = resultJob.Errors,
                    Satatus = (int)HttpStatusCode.InternalServerError
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultJob.Status == ResultStatus.NotFound)
            {
                response = new ApiResponse<bool>(result) {
                    Title = nameof(HttpStatusCode.NotFound),
                    Satatus = (int)HttpStatusCode.NotFound
                };
                return NotFound(response);
            }
            response = new ApiResponse<bool>(result) { 
                Title = nameof(HttpStatusCode.OK),
                Satatus = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Poster")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Detele(int id)
        {
            ApiResponse<bool> response;
            var resultJobImg = await _JobService.GetById(id);
            if(resultJobImg != null)
            {
                if (!string.IsNullOrEmpty(resultJobImg.Value.Img))     
                    await _imageService.Delete(resultJobImg.Value.Img, AplicationConstants.FileContainers.JobImageContainer);             
            }
            var resultJob = await _JobService.Remove(id);
            var result = resultJob.Value;
            if (resultJob.Status == ResultStatus.Error || resultJobImg.Status == ResultStatus.Error)
            {
                response = new ApiResponse<bool>(result) { 
                    Title = nameof(HttpStatusCode.InternalServerError), 
                    Errors = resultJob.Errors,
                    Satatus = (int)HttpStatusCode.InternalServerError
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultJob.Status == ResultStatus.NotFound)
            {
                response = new ApiResponse<bool>(result) {
                    Title = nameof(HttpStatusCode.NotFound),
                    Satatus = (int)HttpStatusCode.NotFound
                };
                return NotFound(response);
            }
            response = new ApiResponse<bool>(result) { 
                Title = nameof(HttpStatusCode.OK),
                Satatus = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }
    }
}
