using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmploymentApp.Core.Entities
{
    public partial class UserLogin
    {
        public int UserLoginId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
