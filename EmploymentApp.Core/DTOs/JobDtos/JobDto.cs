using Microsoft.AspNetCore.Http;

namespace EmploymentApp.Core.DTOs.JobDtos
{
    public class JobDto:JobBaseDto
    {
        public int? CategoryId { get; set; }
        public int? TypeScheduleId { get; set; }
        public int? StatusId { get; set; }
        public IFormFile Img { get; set; }
    }
}
