using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmploymentApp.Core.Entities
{
    public partial class Job: BaseEntity
    {
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public int TypeScheduleId { get; set; }
        public int StatusId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Status Status { get; set; }
        public virtual TypeSchedule TypeSchedule { get; set; }
    }
}
