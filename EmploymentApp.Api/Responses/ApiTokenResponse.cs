using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.DTOs.UserDtos;

namespace EmploymentApp.Api.Responses
{
    public class ApiTokenResponse: ApiResponseBase
    {
        public Token Token { get; set; }
    }
}
