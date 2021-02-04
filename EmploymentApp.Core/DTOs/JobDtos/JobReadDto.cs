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
        public CategoryReadDto Category { get; set; }
        public  StatusReadDto Status { get; set; }
        public TypeScheduleReadDto TypeSchedule { get; set; }
    }
}
