using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Core.QueryFilters
{
    public class UserQueryFilter: BaseQueryFilter 
    {
        public string Name { get; set; }
        public string Surnames { get; set; }
        public DateTime? Bithdate { get; set; }
    }
}
