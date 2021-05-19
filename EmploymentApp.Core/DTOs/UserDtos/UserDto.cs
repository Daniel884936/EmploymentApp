using Microsoft.AspNetCore.Http;
using System;

namespace EmploymentApp.Core.DTOs.UserDtos
{
    public class UserDto
    {
        public IFormFile Img { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public DateTime? Bithdate { get; set; }
    }
}
