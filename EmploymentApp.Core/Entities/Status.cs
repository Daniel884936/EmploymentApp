using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmploymentApp.Core.Entities
{
    public partial class Status
    {
        public Status()
        {
            Job = new HashSet<Job>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
