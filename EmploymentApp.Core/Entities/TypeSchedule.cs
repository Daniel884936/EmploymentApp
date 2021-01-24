using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmploymentApp.Core.Entities
{
    public partial class TypeSchedule
    {
        public TypeSchedule()
        {
            Job = new HashSet<Job>();
        }

        public int TypeScheduleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
