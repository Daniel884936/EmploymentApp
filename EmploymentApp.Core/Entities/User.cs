using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmploymentApp.Core.Entities
{
    public partial class User
    {
        public User()
        {
            UserLogin = new HashSet<UserLogin>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public DateTime Bithdate { get; set; }

        public virtual ICollection<UserLogin> UserLogin { get; set; }
    }
}
