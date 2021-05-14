using System.Collections.Generic;

namespace EmploymentApp.Api.Responses
{
    public abstract class ApiResponseBase
    {
        public string Title { get; set; }
        public int Satatus { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
