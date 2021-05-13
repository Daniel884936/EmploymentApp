using EmploymentApp.Core.DTOs.CategoryDto;
using EmploymentApp.Core.DTOs.StatusDtos;
using EmploymentApp.Core.DTOs.TypeScheduleDto;
using System;

namespace EmploymentApp.Core.DTOs.JobDtos
{
    public class JobReadDto:JobBaseDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Img { get; set; }
        public string TypeSchedule { get; set; }
    }
}
