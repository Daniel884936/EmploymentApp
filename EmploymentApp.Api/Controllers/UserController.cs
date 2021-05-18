using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source.Enums;
using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.DTOs.UserDtos;
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
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmploymentApp.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserController(IUserService userService, IMapper mapper, IUriService uriService)
        {
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
            
        }

        [HttpGet]
        [Authorize(Roles = nameof (Roles.Admin))]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<UserReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<UserReadDto>>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetUsers([FromQuery] UserQueryFilter filter)
        {
            ApiResponse<IEnumerable<UserReadDto>> response;
            var resultUser = _userService.GetAll(filter);
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiPagedResponse<IEnumerable<UserReadDto>>(Array.Empty<UserReadDto>()) 
                { 
                    Title = nameof(HttpStatusCode.InternalServerError), 
                    Errors = resultUser.Errors
                }; 
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var users = resultUser.Value;
            var meta = _mapper.Map<Metadata>(users);
            meta.NextPageUrl = _uriService.GetPaginationNextUrl(filter, Request, meta.HasNextPage);
            meta.PreviousPageUrl = _uriService.GetPaginationPreviousUrl(filter, Request, meta.HasPreviousPage);
            var usersReadDto = _mapper.Map<IEnumerable<UserReadDto>>(users);
            response = new ApiPagedResponse<IEnumerable<UserReadDto>>(usersReadDto)
            { 
                Title = nameof(HttpStatusCode.OK), 
                Meta = meta,
                Satatus = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }

        
        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Roles.Admin))]
        [ProducesResponseType(typeof(ApiResponse<UserReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UserReadDto>),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(int id)
        {
            ApiResponse<UserReadDto> response;
            var resultUser = await _userService.GetById(id);
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiResponse<UserReadDto>(null) {
                    Title = nameof(HttpStatusCode.InternalServerError), 
                    Errors = resultUser.Errors
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var user = resultUser.Value;
            var userReadDto = _mapper.Map<UserReadDto>(user);
            userReadDto.Email = user.UserLogin.ElementAt((int)UserLoginNum.First).Email;
            response = new ApiResponse<UserReadDto>(userReadDto) {
                Title = nameof(HttpStatusCode.OK),
                Satatus = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UserReadDto>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<UserReadDto>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromForm]UserCreateDto userCreateDto)
        {
            ApiResponse<UserReadDto> response;
            var user = _mapper.Map<User>(userCreateDto);
            var resultUser = await _userService.Add(user);
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiResponse<UserReadDto>(null) { 
                    Title = nameof(HttpStatusCode.InternalServerError), 
                    Errors = resultUser.Errors
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultUser.Status == ResultStatus.Invalid)
            {
                response = new ApiResponse<UserReadDto>(null) { 
                    Title = nameof(HttpStatusCode.Conflict),
                    Errors = resultUser.ValidationErrors.Select(x=> x.ErrorMessage),
                    Satatus = (int)HttpStatusCode.Conflict
                };
                return Conflict(response);
            }
            var userReadDto = _mapper.Map<UserReadDto>(user);
            response = new ApiResponse<UserReadDto>(userReadDto){
                Title = nameof(HttpStatusCode.OK),
                Satatus = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles ="Admin,Poster")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, UserDto userDto)
        {
            ApiResponse<bool> response;
            var user = _mapper.Map<User>(userDto);
            user.Id = id;
            var resultUser = await _userService.Update(user);
            var result = resultUser.Value;
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiResponse<bool>(result) { 
                    Title = nameof(HttpStatusCode.InternalServerError), 
                    Errors = resultUser.Errors
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultUser.Status == ResultStatus.NotFound)
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
            var resultUser = await _userService.Remove(id);
            var result = resultUser.Value;
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiResponse<bool>(result) { 
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors = resultUser.Errors,
                    Satatus = (int)HttpStatusCode.InternalServerError
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultUser.Status == ResultStatus.NotFound)
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
