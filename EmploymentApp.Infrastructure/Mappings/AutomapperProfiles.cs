using AutoMapper;
using EmploymentApp.Core.DTOs.CategoryDto;
using EmploymentApp.Core.DTOs.JobDtos;
using EmploymentApp.Core.DTOs.RoleDtos;
using EmploymentApp.Core.DTOs.StatusDtos;
using EmploymentApp.Core.DTOs.TypeScheduleDto;
using EmploymentApp.Core.Entities;

namespace EmploymentApp.Infrastructure.Mappings
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<Status, StatusReadDto>();
            CreateMap<Role, RoleReadDto>();
            CreateMap<TypeSchedule, TypeScheduleReadDto>();
            CreateMap<JobDto, Job>();
            CreateMap<Job, JobReadDto>();


        }
    }
}
