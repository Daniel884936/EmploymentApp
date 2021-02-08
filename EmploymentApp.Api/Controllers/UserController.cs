using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Api.Source;
using EmploymentApp.Api.Source.Enums;
using EmploymentApp.Core.DTOs.UserDtos;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
      
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            
        }
         [HttpGet]
        public IActionResult GetUsers()
        {
            ApiResponse<IEnumerable<UserReadDto>> response;
            var resultUser = _userService.GetAll();
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiResponse<IEnumerable<UserReadDto>>(Array.Empty<UserReadDto>(),
                    resultUser.Errors.ElementAt((int)ErrorNum.First));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var users = resultUser.Value;
            var usersReadDto = _mapper.Map<IEnumerable<UserReadDto>>(users);
            response = new ApiResponse<IEnumerable<UserReadDto>>(usersReadDto,
                StringResponseMessages.SUCESS);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            ApiResponse<UserReadDto> response;
            var resultUser = await _userService.GetById(id);
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiResponse<UserReadDto>(null,
                    resultUser.Errors.ElementAt((int)ErrorNum.First));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var user = resultUser.Value;
            var userReadDto = _mapper.Map<UserReadDto>(user);
            userReadDto.Email = user.UserLogin.ElementAt((int)UserLoginNum.First).Email;
            response = new ApiResponse<UserReadDto>(userReadDto,
                StringResponseMessages.SUCESS);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserCreateDto userCreateDto)
        {
            ApiResponse<UserReadDto> response;
            var user = _mapper.Map<User>(userCreateDto);
            var resultUser = await _userService.Add(user);
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiResponse<UserReadDto>(null,
                    resultUser.Errors.ElementAt((int)ErrorNum.First));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            var userReadDto = _mapper.Map<UserReadDto>(user);
            response = new ApiResponse<UserReadDto>(userReadDto,
                 StringResponseMessages.SUCESS);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, UserDto userDto)
        {
            ApiResponse<bool> response;
            var user = _mapper.Map<User>(userDto);
            user.Id = id;
            var resultUser = await _userService.Update(user);
            var result = resultUser.Value;
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiResponse<bool>(result,
                    resultUser.Errors.ElementAt((int)ErrorNum.First));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultUser.Status == ResultStatus.NotFound)
            {
                response = new ApiResponse<bool>(result,
                    StringResponseMessages.DOES_NOT_EXIST);
                return NotFound(response);
            }
            response = new ApiResponse<bool>(result,
                 StringResponseMessages.SUCESS);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Detele(int id)
        {
            ApiResponse<bool> response;
            var resultUser = await _userService.Remove(id);
            var result = resultUser.Value;
            if (resultUser.Status == ResultStatus.Error)
            {
                response = new ApiResponse<bool>(result,
                    resultUser.Errors.ElementAt((int)ErrorNum.First));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (resultUser.Status == ResultStatus.NotFound)
            {
                response = new ApiResponse<bool>(result,
                    StringResponseMessages.DOES_NOT_EXIST);
                return NotFound(response);
            }
            response = new ApiResponse<bool>(result,
                 StringResponseMessages.SUCESS);
            return Ok(response);
        }
    }
}
