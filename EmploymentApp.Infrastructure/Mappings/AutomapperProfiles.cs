using AutoMapper;
using EmploymentApp.Core.DTOs;
using EmploymentApp.Core.DTOs.CategoryDto;
using EmploymentApp.Core.DTOs.RoleDtos;
using EmploymentApp.Core.DTOs.StatusDtos;
using EmploymentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Infrastructure.Mappings
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<Status, StatusReadDto>();
            CreateMap<Role, RoleReadDto>();

        }
    }
}
