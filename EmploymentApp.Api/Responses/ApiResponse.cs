using System.Collections.Generic;

namespace EmploymentApp.Api.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public ApiResponse(T data )
        {
            Data = data;
        }
    }
}
