using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Api.Source.Enums;
using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.DTOs.UserDtos;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private string responseMessage;

        public UserController(IUserService userService, IMapper mapper, IUriService uriService)
        {
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
            
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<UserReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUsers([FromQuery] UserQueryFilter filter)
        {
            ApiResponse<IEnumerable<UserReadDto>> response;
            var resultUser = _userService.GetAll(filter);
            if (resultUser.Status == ResultStatus.Error)
            {
                responseMessage = resultUser.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiPagedResponse<IEnumerable<UserReadDto>>(Array.Empty<UserReadDto>()) 
                { 
                    Message = responseMessage 
                }; 
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var users = resultUser.Value;
            var meta = _mapper.Map<Metadata>(users);
            meta.NextPageUrl = _uriService.GetPaginationNextUrl(filter, Request, meta.HasNextPage);
            meta.PreviousPageUrl = _uriService.GetPaginationPreviousUrl(filter, Request, meta.HasPreviousPage);
            var usersReadDto = _mapper.Map<IEnumerable<UserReadDto>>(users);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiPagedResponse<IEnumerable<UserReadDto>>(usersReadDto)
            { 
                Message = responseMessage, 
                Meta = meta
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<UserReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(int id)
        {
            ApiResponse<UserReadDto> response;
            var resultUser = await _userService.GetById(id);
            if (resultUser.Status == ResultStatus.Error)
            {
                responseMessage = resultUser.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<UserReadDto>(null) { Message = responseMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var user = resultUser.Value;
            var userReadDto = _mapper.Map<UserReadDto>(user);
            userReadDto.Email = user.UserLogin.ElementAt((int)UserLoginNum.First).Email;
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<UserReadDto>(userReadDto) {Message = responseMessage };
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(UserCreateDto userCreateDto)
        {
            ApiResponse<UserReadDto> response;
            var user = _mapper.Map<User>(userCreateDto);
            var resultUser = await _userService.Add(user);
            if (resultUser.Status == ResultStatus.Error)
            {
                responseMessage = resultUser.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<UserReadDto>(null) { Message = responseMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultUser.Status == ResultStatus.Invalid)
            {
                responseMessage = resultUser.ValidationErrors.ElementAt((int)ErrorNum.First).ErrorMessage;
                response = new ApiResponse<UserReadDto>(null) { Message = responseMessage };
                return Conflict(response);
            }
            var userReadDto = _mapper.Map<UserReadDto>(user);
            responseMessage = StringResponseMessages.SUCESS;
            response = new ApiResponse<UserReadDto>(userReadDto) { Message = responseMessage };
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UserDto userDto)
        {
            ApiResponse<bool> response;
            var user = _mapper.Map<User>(userDto);
            user.Id = id;
            var resultUser = await _userService.Update(user);
            var result = resultUser.Value;
            if (resultUser.Status == ResultStatus.Error)
            {
                responseMessage = resultUser.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<bool>(result) { Message = responseMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultUser.Status == ResultStatus.NotFound)
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
            var resultUser = await _userService.Remove(id);
            var result = resultUser.Value;
            if (resultUser.Status == ResultStatus.Error)
            {
                responseMessage = resultUser.Errors.ElementAt((int)ErrorNum.First);
                response = new ApiResponse<bool>(result) { Message = responseMessage };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultUser.Status == ResultStatus.NotFound)
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
