using System;

namespace EmploymentApp.Core.DTOs.UserDtos
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public DateTime? Bithdate { get; set; }
        //public RoleReadDto Role { get; set; }
    }
}
