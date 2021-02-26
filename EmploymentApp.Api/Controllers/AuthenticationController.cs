using Ardalis.Result;
using AutoMapper;
using EmploymentApp.Api.Responses;
using EmploymentApp.Core.DTOs.UserDtos;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace EmploymentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserLoginService _userLoginService;
        private readonly IMapper _mapper;
        public AuthenticationController( ITokenService tokenService, IUserLoginService userLoginService,IMapper mapper)
        {
            _tokenService = tokenService;
            _userLoginService = userLoginService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiTokenResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiTokenResponse),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiTokenResponse), StatusCodes.Status500InternalServerError)]
        public async Task< IActionResult> Authenticate(UserLoginDto userLoginDto)
        {
            ApiTokenResponse response;
            var userLogin = _mapper.Map<UserLoginDto, UserLogin>(userLoginDto);
            var resultUserLogin = await _userLoginService.GetByCredentials(userLogin);
            userLogin = resultUserLogin.Value;
            if(resultUserLogin.Status == ResultStatus.Error)
            {
                response = new ApiTokenResponse
                {
                    Title = nameof(HttpStatusCode.InternalServerError),
                    Errors = resultUserLogin.Errors
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            if (userLogin == null)
            {
                response = new ApiTokenResponse{ Title = nameof(HttpStatusCode.NotFound)};
                return NotFound(response);
            }
             response = new ApiTokenResponse
            {
                Token = _tokenService.GenerateToken(userLogin.User), 
                Title = nameof(HttpStatusCode.OK)
             };

            return Ok(response);
        }
    }
}
