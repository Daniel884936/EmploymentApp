using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Core.QueryFilters
{
    public abstract class BaseQueryFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
