using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Infrastructure.Options
{
    public class FileOptions
    {
        public int MaxKb { get; set; }
        public string[] ValidTypes { get; set; }
    }
}
