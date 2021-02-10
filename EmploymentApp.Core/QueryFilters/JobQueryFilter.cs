using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Core.QueryFilters
{
    public class JobQueryFilter
    {
        public string Company { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public string Category { get; set; }
        public string TypeSchedule { get; set; }
    }
}
