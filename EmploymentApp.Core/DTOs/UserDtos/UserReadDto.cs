using EmploymentApp.Core.DTOs.RoleDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Core.DTOs.UserDtos
{
    public class UserReadDto: UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        //public RoleReadDto Role { get; set; }
    }
}
