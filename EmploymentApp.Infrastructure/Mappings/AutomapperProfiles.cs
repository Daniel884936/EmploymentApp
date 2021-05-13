using AutoMapper;
using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.DTOs.CategoryDto;
using EmploymentApp.Core.DTOs.JobDtos;
using EmploymentApp.Core.DTOs.RoleDtos;
using EmploymentApp.Core.DTOs.StatusDtos;
using EmploymentApp.Core.DTOs.TypeScheduleDto;
using EmploymentApp.Core.DTOs.UserDtos;
using EmploymentApp.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EmploymentApp.Infrastructure.Mappings
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            MapCategory();
            MapStatus();
            MapTypeSchedule();
            MapRole();
            MapJob();
            MapUser();
            MapMetadata();
            MapUserLogin();
        }
        private void MapCategory()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryReadDto>();
        }
        private void MapStatus() => CreateMap<Status, StatusReadDto>();
        private void MapRole() => CreateMap<Role, RoleReadDto>();
        private void MapTypeSchedule() => CreateMap<TypeSchedule, TypeScheduleReadDto>();
        private void MapJob()
        {
            CreateMap<JobDto, Job>()
                .ForMember(dest => dest.Img, opt => opt.Ignore());

            CreateMap<Job, JobReadDto>().ForMember(dest =>
            dest.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(dest =>
            dest.Status, opt => opt.MapFrom(x => x.Status.Name))
                .ForMember(dest =>
            dest.TypeSchedule, opt => opt.MapFrom(x => x.TypeSchedule.Name));
        }
        private void MapUser()
        {
           CreateMap<User, UserReadDto>().ForMember(dest =>
           dest.Email, opt => opt.MapFrom(x =>
           x.UserLogin.ElementAt(0).Email)).ForMember(dest =>
           dest.Role, opt => opt.MapFrom(x =>
           x.UserLogin.ElementAt(0).Role.Name));

           CreateMap<UserCreateDto, User>().ForMember(dest => dest.UserLogin
           , opt => opt.MapFrom(x => new List<UserLogin> {
               new UserLogin {
                   Password = x.Password,
                   Email = x.Email,
                   RoleId = (int)x.RoleId
               }
           }));
           CreateMap<UserDto, User>();
        }

        private void MapMetadata()
        {
            CreateMap<PagedList<Job>, Metadata>();
            CreateMap<PagedList<User>, Metadata>();
        }

        private void MapUserLogin()
        {
            CreateMap<UserLoginDto, UserLogin>();
        }
    }
}
