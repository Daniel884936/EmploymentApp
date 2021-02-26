using System.Collections.Generic;

namespace EmploymentApp.Api.Responses
{
    public class ApiResponse<T>: ApiResponseBase
    {
        public T Data { get; set; }
        public ApiResponse(T data )
        {
            Data = data;
        }
    }
}
