using Microsoft.AspNetCore.Http;

namespace EmploymentApp.Core.DTOs.JobDtos
{
    public abstract class JobBaseDto
    {
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}
