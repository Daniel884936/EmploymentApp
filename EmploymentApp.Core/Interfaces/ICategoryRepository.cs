using EmploymentApp.Core.Entities;
using System.Collections.Generic;

namespace EmploymentApp.Core.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
    }
}