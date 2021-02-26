using EmploymentApp.Core.CustomEntities;

namespace EmploymentApp.Api.Responses
{
    public class ApiPagedResponse<T>: ApiResponse<T>
    {
        public ApiPagedResponse(T data):base(data){}
        public Metadata Meta { get; set; }
    }
}
