using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace EmploymentApp.Infrastructure.Validators
{
    public class FileValidator
    {
        public static bool ValidFileType(IFormFile file, string[] validTypes)
        {
            if (file != null)
                if (validTypes.Contains(file.ContentType)) return true;
            if (file == null) return true;
            return false;
        }

        public static bool LessThan(IFormFile file, int kbSize)
        {
            if (file != null)          
                if (file.Length / 1000 <= kbSize) return true;       
            if (file == null) return true;
            return false;
        }
    }
}
