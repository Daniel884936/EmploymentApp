namespace EmploymentApp.Core.DTOs.UserDtos
{
    public class UserCreateDto: UserDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
    }
}
