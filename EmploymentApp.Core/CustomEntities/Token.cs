using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Core.CustomEntities
{
    public class Token
    {
        public string Data { get; set; }
        public DateTime DateToExpire { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
