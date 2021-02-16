using EmploymentApp.Core.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmploymentApp.Api.Responses
{
    public class ApiPagedResponse<T>: ApiResponse<T>
    {
        public ApiPagedResponse(T data):base(data){}
        public Metadata Meta { get; set; }
    }
}
